using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using backend.DTOs.Media;
using backend.Models;
using backend.Repository.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class MediaController : ControllerBase
    {
        private readonly IMediaRepository _mediaRepo;
        private readonly IWebHostEnvironment _env;

        public MediaController(IMediaRepository mediaRepo, IWebHostEnvironment env)
        {
            _mediaRepo = mediaRepo;
            _env = env;
        }

        [HttpPost("upload/init")]
        public async Task<IActionResult> InitUpload([FromBody] ChunkUploadInitDto model)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var asset = new TaiNguyenSo
            {
                TaiLenBoi = userId,
                TenTep = model.FileName,
                LoaiTep =
                    model.ContentType.StartsWith("video/") ? "Video"
                    : model.ContentType.StartsWith("image/") ? "Image"
                    : "Document",
                KieuMIME = model.ContentType,
                DungLuongByte = model.FileSize,
                TrangThai = "Pending",
                DuongDanLuuTru = "",
            };

            var createdAsset = await _mediaRepo.CreateAssetAsync(asset);

            var tempDir = Path.Combine(
                _env.ContentRootPath,
                "Temp",
                "Uploads",
                createdAsset.MaTaiNguyen.ToString()
            );
            if (!Directory.Exists(tempDir))
                Directory.CreateDirectory(tempDir);

            return Ok(new { assetId = createdAsset.MaTaiNguyen });
        }

        [HttpPost("upload/chunk")]
        public async Task<IActionResult> UploadChunk([FromForm] ChunkUploadDto model)
        {
            var tempDir = Path.Combine(
                _env.ContentRootPath,
                "Temp",
                "Uploads",
                model.AssetId.ToString()
            );
            if (!Directory.Exists(tempDir))
                return NotFound(new { message = "Upload session not found." });

            var chunkPath = Path.Combine(tempDir, model.ChunkIndex.ToString());

            using (var stream = new FileStream(chunkPath, FileMode.Create))
            {
                await model.ChunkData.CopyToAsync(stream);
            }

            return Ok(new { message = $"Chunk {model.ChunkIndex} uploaded successfully." });
        }

        [HttpPost("upload/complete/{assetId}")]
        public async Task<IActionResult> CompleteUpload(
            Guid assetId,
            [FromQuery] string fileExtension,
            [FromServices] IStorageService storageService
        )
        {
            var asset = await _mediaRepo.GetAssetByIdAsync(assetId);
            if (asset == null)
                return NotFound("Asset not found.");

            var tempDir = Path.Combine(_env.ContentRootPath, "Temp", "Uploads", assetId.ToString());
            if (!Directory.Exists(tempDir))
                return NotFound("No chunks found.");

            string finalFileName = $"{assetId}{fileExtension}";
            var finalPath = Path.Combine(_env.ContentRootPath, "Temp", "Uploads", finalFileName);

            var chunkFiles = Directory
                .GetFiles(tempDir)
                .OrderBy(f => int.Parse(Path.GetFileName(f)))
                .ToList();

            using (var finalStream = new FileStream(finalPath, FileMode.Create))
            {
                foreach (var chunkFile in chunkFiles)
                {
                    using (var chunkStream = new FileStream(chunkFile, FileMode.Open))
                    {
                        await chunkStream.CopyToAsync(finalStream);
                    }
                }
            }

            Directory.Delete(tempDir, true);

            asset.OriginalFilePath = finalPath;

            if (asset.LoaiTep != "Video")
            {
                // Upload trực tiếp tài liệu vào R2
                using var stream = new FileStream(finalPath, FileMode.Open);
                string fileKey = $"documents/{assetId}{fileExtension}";
                await storageService.UploadFileAsync(stream, fileKey, asset.KieuMIME);

                asset.DuongDanLuuTru = fileKey;
                asset.TrangThai = "Ready";

                stream.Close();
                System.IO.File.Delete(finalPath);
            }
            else
            {
                // Video thì đưa vào chế độ Nén (Processing)
                asset.TrangThai = "Processing";
            }

            await _mediaRepo.UpdateAssetAsync(asset);

            return Ok(
                new
                {
                    message = "Upload complete.",
                    assetId = asset.MaTaiNguyen,
                    status = asset.TrangThai,
                }
            );
        }

        [HttpGet("{assetId}/key")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEncryptionKey(Guid assetId)
        {
            // Bảo mật cực mạnh: Chỉ khi có Access Token hợp lệ, Backend mới lấy chìa khóa giao cho Frontend.
            var asset = await _mediaRepo.GetAssetByIdAsync(assetId);
            if (asset == null || string.IsNullOrEmpty(asset.AesKey))
                return NotFound("Key not found.");

            // TODO: Bạn có thể thêm lệnh check "User này có mua khóa học chứa assetId này chưa" ở đây để bảo mật x2.

            var keyBytes = Convert.FromBase64String(asset.AesKey);
            return File(keyBytes, "application/octet-stream");
        }

        [HttpGet("{assetId}/download-url")]
        public async Task<IActionResult> GetDownloadUrl(
            Guid assetId,
            [FromServices] IStorageService storageService
        )
        {
            var asset = await _mediaRepo.GetAssetByIdAsync(assetId);
            if (asset == null || asset.TrangThai != "Ready")
                return NotFound("Asset not found or not ready.");

            // Tạo Presigned URL (có hạn 60 phút) cho cả video và tài liệu
            var presignedUrl = storageService.GeneratePresignedUrl(asset.DuongDanLuuTru, 60);
            return Ok(new { url = presignedUrl, type = asset.LoaiTep });
        }

        // Stream proxy - hỗ trợ cả MP4 và HLS
        [HttpGet("stream/{assetId}")]
        [AllowAnonymous]
        public async Task<IActionResult> StreamVideo(
            Guid assetId,
            [FromServices] IStorageService storageService
        )
        {
            try
            {
                var asset = await _mediaRepo.GetAssetByIdAsync(assetId);
                if (asset == null || asset.TrangThai != "Ready" || string.IsNullOrEmpty(asset.DuongDanLuuTru))
                    return NotFound(new { message = "Asset not found or not ready." });

                var stream = await storageService.GetFileStreamAsync(asset.DuongDanLuuTru);

                var contentType = asset.DuongDanLuuTru.EndsWith(".m3u8")
                    ? "application/vnd.apple.mpegurl"
                    : asset.KieuMIME ?? "video/mp4";

                return File(stream, contentType, enableRangeProcessing: true);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = "File not found in R2", error = ex.Message });
            }
        }

        // Lấy từng file HLS (dành cho HLS player)
        [HttpGet("stream/{assetId}/{fileName}")]
        [AllowAnonymous]
        public async Task<IActionResult> StreamHlsFile(
            Guid assetId,
            string fileName,
            [FromServices] IStorageService storageService
        )
        {
            try
            {
                // Thử path HLS trước, nếu không có thì thử path video
                var fileKey = $"hls/{assetId}/{fileName}";
                var stream = await storageService.GetFileStreamAsync(fileKey);

                var contentType = fileName.EndsWith(".m3u8")
                    ? "application/vnd.apple.mpegurl"
                    : "video/MP2T";

                return File(stream, contentType, enableRangeProcessing: true);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = "File not found in R2", error = ex.Message });
            }
        }

        // --- CÁC API DÀNH CHO QUẢN TRỊ TÀI NGUYÊN SỐ (ASSET MANAGER) ---

        [HttpGet("my-assets")]
        public async Task<IActionResult> GetMyAssets()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var assets = await _mediaRepo.GetAssetsByUserIdAsync(userId);
            return Ok(assets);
        }

        [HttpDelete("{assetId}")]
        public async Task<IActionResult> DeleteAsset(
            Guid assetId,
            [FromServices] IStorageService storageService
        )
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var asset = await _mediaRepo.GetAssetByIdAsync(assetId);
            if (asset == null)
                return NotFound("Asset not found.");

            if (asset.TaiLenBoi != userId)
                return Forbid();

            // Xóa file vật lý trên R2
            if (!string.IsNullOrEmpty(asset.DuongDanLuuTru) || asset.LoaiTep == "Video")
            {
                if (asset.LoaiTep == "Video")
                {
                    await storageService.DeleteFolderAsync($"hls/{assetId}/");
                }
                else
                {
                    await storageService.DeleteFileAsync(asset.DuongDanLuuTru);
                }
            }

            // Xóa file rác trong Temp nếu có
            if (
                !string.IsNullOrEmpty(asset.OriginalFilePath)
                && System.IO.File.Exists(asset.OriginalFilePath)
            )
            {
                try
                {
                    System.IO.File.Delete(asset.OriginalFilePath);
                }
                catch { }
            }

            // Xóa trong DB
            await _mediaRepo.DeleteAssetAsync(assetId);

            return Ok(new { message = "Đã xóa Tài nguyên số thành công" });
        }
    }
}
