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

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(
            IFormFileCollection files,
            [FromServices] IStorageService storageService
        )
        {
            if (files == null || files.Count == 0)
                return BadRequest("No files uploaded.");

            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var results = new List<object>();

            foreach (var file in files)
            {
                if (file.Length == 0) continue;

                // 1. Tạo bản ghi tài nguyên trong DB
                var assetId = Guid.NewGuid();
                var fileExtension = Path.GetExtension(file.FileName);
                var fileKey = file.ContentType.StartsWith("image/") 
                    ? $"images/{assetId}{fileExtension}" 
                    : $"documents/{assetId}{fileExtension}";

                var asset = new TaiNguyenSo
                {
                    MaTaiNguyen = assetId,
                    TaiLenBoi = userId,
                    TenTep = file.FileName,
                    LoaiTep = file.ContentType.StartsWith("image/") ? "Image" : "Document",
                    KieuMIME = file.ContentType,
                    DungLuongByte = file.Length,
                    TrangThai = "Pending",
                    DuongDanLuuTru = fileKey,
                    NgayTao = DateTime.Now
                };

                await _mediaRepo.CreateAssetAsync(asset);

                // 2. Upload trực tiếp lên Cloud R2
                using (var stream = file.OpenReadStream())
                {
                    await storageService.UploadFileAsync(stream, fileKey, file.ContentType);
                }

                // 3. Cập nhật trạng thái Ready
                asset.TrangThai = "Ready";
                await _mediaRepo.UpdateAssetAsync(asset);

                results.Add(new { 
                    fileName = file.FileName,
                    assetId = asset.MaTaiNguyen,
                    url = storageService.GeneratePresignedUrl(fileKey, 60)
                });
            }

            return Ok(new { 
                message = $"Successfully uploaded {results.Count} files.", 
                data = results
            });
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

            // Xóa file vật lý trên R2 dựa trên đường dẫn lưu trữ thực tế
            if (!string.IsNullOrEmpty(asset.DuongDanLuuTru))
            {
                // Nếu đường dẫn là thư mục (HLS), xóa cả thư mục
                if (asset.DuongDanLuuTru.Contains("/hls/"))
                {
                    var folderPath = asset.DuongDanLuuTru.Substring(0, asset.DuongDanLuuTru.LastIndexOf('/') + 1);
                    await storageService.DeleteFolderAsync(folderPath);
                }
                else
                {
                    // Nếu là file đơn (MP4, PDF...), xóa file đó
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

        [HttpPut("{assetId}/replace")]
        public async Task<IActionResult> ReplaceAsset(
            Guid assetId,
            [FromBody] ChunkUploadInitDto model,
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

            // 1. Xóa file cũ trên Cloud để dọn dẹp dung lượng
            if (!string.IsNullOrEmpty(asset.DuongDanLuuTru))
            {
                if (asset.DuongDanLuuTru.Contains("/hls/"))
                {
                    var folderPath = asset.DuongDanLuuTru.Substring(0, asset.DuongDanLuuTru.LastIndexOf('/') + 1);
                    await storageService.DeleteFolderAsync(folderPath);
                }
                else
                {
                    await storageService.DeleteFileAsync(asset.DuongDanLuuTru);
                }
            }

            // 2. Cập nhật thông tin mới và chuyển trạng thái về Pending
            asset.TenTep = model.FileName;
            asset.DungLuongByte = model.FileSize;
            asset.KieuMIME = model.ContentType;
            asset.LoaiTep = model.ContentType.StartsWith("video/") ? "Video"
                            : model.ContentType.StartsWith("image/") ? "Image"
                            : "Document";
            asset.TrangThai = "Pending";
            asset.DuongDanLuuTru = "";
            asset.NgayCapNhat = DateTime.Now;

            await _mediaRepo.UpdateAssetAsync(asset);

            // 3. Chuẩn bị thư mục Temp cho phiên upload mới
            var tempDir = Path.Combine(_env.ContentRootPath, "Temp", "Uploads", assetId.ToString());
            if (Directory.Exists(tempDir)) Directory.Delete(tempDir, true);
            Directory.CreateDirectory(tempDir);

            return Ok(new { message = "Thay thế tài nguyên thành công. Bạn có thể bắt đầu upload file mới.", assetId = asset.MaTaiNguyen });
        }
    }
}
