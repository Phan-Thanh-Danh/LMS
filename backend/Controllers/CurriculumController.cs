using System;
using System.IO;
using System.Threading.Tasks;
using backend.DTOs.Course;
using backend.Models;
using backend.Repository.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/curriculum")]
    [Authorize(Roles = "Instructor,Admin,CMO,Moderator")]
    public class CurriculumController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IWebHostEnvironment _env;

        public CurriculumController(ICourseRepository courseRepository, IWebHostEnvironment env)
        {
            _courseRepository = courseRepository;
            _env = env;
        }

        // ── Chương (Sections) ─────────────────────────────────────

        [HttpGet("courses/{courseId}/sections")]
        public async Task<IActionResult> GetSections(Guid courseId)
        {
            var sections = await _courseRepository.GetSectionsByCourseAsync(courseId);
            var result = sections.Select(s => new SectionResponse
            {
                MaChuong = s.MaChuong,
                MaKhoaHoc = s.MaKhoaHoc,
                TieuDe = s.TieuDe,
                MoTa = s.MoTa,
                ThuTu = s.ThuTu,
                NgayTao = s.NgayTao,
            });
            return Ok(result);
        }

        [HttpPost("courses/{courseId}/sections")]
        public async Task<IActionResult> CreateSection(
            Guid courseId,
            [FromBody] CreateSectionRequest request
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var course = await _courseRepository.GetCourseByIdAsync(courseId);
            if (course == null)
                return NotFound(new { message = "Không tìm thấy khóa học" });
            if (!IsOwnerOrAdmin(course))
                return Forbid();
            if (course.TrangThai == 1)
                return BadRequest(
                    new { message = "Không thể chỉnh sửa khi khóa học đang chờ duyệt" }
                );

            var section = new Chuong
            {
                MaKhoaHoc = courseId,
                TieuDe = request.TieuDe,
                MoTa = request.MoTa,
                ThuTu = request.ThuTu,
            };

            await _courseRepository.AddSectionAsync(section);
            return CreatedAtAction(
                nameof(GetSections),
                new { courseId },
                new SectionResponse
                {
                    MaChuong = section.MaChuong,
                    MaKhoaHoc = section.MaKhoaHoc,
                    TieuDe = section.TieuDe,
                    ThuTu = section.ThuTu,
                    NgayTao = section.NgayTao,
                }
            );
        }

        [HttpPut("sections/{id}")]
        public async Task<IActionResult> UpdateSection(
            int id,
            [FromBody] CreateSectionRequest request
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var section = await _courseRepository.GetSectionByIdAsync(id);
            if (section == null)
                return NotFound(new { message = "Không tìm thấy chương" });
            if (!IsOwnerOrAdmin(section.KhoaHoc))
                return Forbid();

            section.TieuDe = request.TieuDe;
            section.MoTa = request.MoTa;
            section.ThuTu = request.ThuTu;
            await _courseRepository.UpdateSectionAsync(section);
            return Ok(new { message = "Cập nhật chương thành công" });
        }

        [HttpDelete("sections/{id}")]
        public async Task<IActionResult> DeleteSection(int id)
        {
            var section = await _courseRepository.GetSectionByIdAsync(id);
            if (section == null)
                return NotFound(new { message = "Không tìm thấy chương" });
            if (!IsOwnerOrAdmin(section.KhoaHoc))
                return Forbid();

            await _courseRepository.DeleteSectionAsync(id);
            return Ok(new { message = "Đã xóa chương thành công" });
        }

        // ── Bài giảng (Lectures) ──────────────────────────────────

        [HttpGet("sections/{sectionId}/lectures")]
        public async Task<IActionResult> GetLectures(int sectionId)
        {
            var lectures = await _courseRepository.GetLecturesBySectionAsync(sectionId);
            var result = lectures.Select(l => new LectureResponse
            {
                MaBaiGiang = l.MaBaiGiang,
                MaChuong = l.MaChuong,
                TieuDe = l.TieuDe,
                LoaiBaiGiang = l.LoaiBaiGiang,
                MoTa = l.MoTa,
                ThuTu = l.ThuTu,
                ThoiLuong = l.ThoiLuong,
                XemMienPhi = l.XemMienPhi,
                DangKhoa = l.DangKhoa,
                MaTaiNguyen = l.MaTaiNguyen,
                TrangThaiTaiNguyen = l.TaiNguyenSo?.TrangThai,
                NgayTao = l.NgayTao,
            });
            return Ok(result);
        }

        [HttpPost("sections/{sectionId}/lectures")]
        public async Task<IActionResult> CreateLecture(
            int sectionId,
            [FromBody] CreateLectureRequest request
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var section = await _courseRepository.GetSectionByIdAsync(sectionId);
            if (section == null)
                return NotFound(new { message = "Không tìm thấy chương" });
            if (!IsOwnerOrAdmin(section.KhoaHoc))
                return Forbid();

            var lecture = new BaiGiang
            {
                MaChuong = sectionId,
                TieuDe = request.TieuDe,
                LoaiBaiGiang = request.LoaiBaiGiang,
                MoTa = request.MoTa,
                ThuTu = request.ThuTu,
                XemMienPhi = request.XemMienPhi,
                MaTaiNguyen = request.MaTaiNguyen,
            };

            await _courseRepository.AddLectureAsync(lecture);
            return Ok(
                new { message = "Thêm bài giảng thành công", maBaiGiang = lecture.MaBaiGiang }
            );
        }

        [HttpPut("lectures/{id}")]
        public async Task<IActionResult> UpdateLecture(
            int id,
            [FromBody] CreateLectureRequest request,
            [FromServices] IStorageService storageService,
            [FromServices] IMediaRepository mediaRepository
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var lecture = await _courseRepository.GetLectureByIdAsync(id);
            if (lecture == null)
                return NotFound(new { message = "Không tìm thấy bài giảng" });

            var section = await _courseRepository.GetSectionByIdAsync(lecture.MaChuong);
            if (section == null || !IsOwnerOrAdmin(section.KhoaHoc))
                return Forbid();

            // Nếu giảng viên thay đổi File/Video, ta phải xóa file cũ đi để tiết kiệm Cloudflare R2
            if (lecture.MaTaiNguyen != request.MaTaiNguyen)
            {
                if (lecture.MaTaiNguyen.HasValue && lecture.TaiNguyenSo != null)
                {
                    if (lecture.TaiNguyenSo.LoaiTep == "Video")
                    {
                        // Folder HLS
                        await storageService.DeleteFolderAsync($"hls/{lecture.MaTaiNguyen.Value}/");
                    }
                    else if (!string.IsNullOrEmpty(lecture.TaiNguyenSo.DuongDanLuuTru))
                    {
                        // Document / PDF / Image
                        await storageService.DeleteFileAsync(lecture.TaiNguyenSo.DuongDanLuuTru);
                    }
                    
                    // Xóa metadata trong bảng TaiNguyenSo
                    await mediaRepository.DeleteAssetAsync(lecture.MaTaiNguyen.Value);
                }
                
                lecture.MaTaiNguyen = request.MaTaiNguyen;
            }

            lecture.TieuDe = request.TieuDe;
            lecture.LoaiBaiGiang = request.LoaiBaiGiang;
            lecture.MoTa = request.MoTa;
            lecture.ThuTu = request.ThuTu;
            lecture.XemMienPhi = request.XemMienPhi;

            await _courseRepository.UpdateLectureAsync(lecture);
            return Ok(new { message = "Cập nhật bài giảng thành công" });
        }

        [HttpDelete("lectures/{id}")]
        public async Task<IActionResult> DeleteLecture(
            int id,
            [FromServices] IStorageService storageService,
            [FromServices] IMediaRepository mediaRepository
        )
        {
            var lecture = await _courseRepository.GetLectureByIdAsync(id);
            if (lecture == null)
                return NotFound(new { message = "Không tìm thấy bài giảng" });

            var section = await _courseRepository.GetSectionByIdAsync(lecture.MaChuong);
            if (section == null || !IsOwnerOrAdmin(section.KhoaHoc))
                return Forbid();

            var oldMediaId = lecture.MaTaiNguyen;
            var oldMedia = lecture.TaiNguyenSo;

            await _courseRepository.DeleteLectureAsync(id);

            // Tự dọn dẹp R2 Cloudflare khi xóa bài giảng
            if (oldMediaId.HasValue && oldMedia != null)
            {
                if (oldMedia.LoaiTep == "Video")
                {
                    await storageService.DeleteFolderAsync($"hls/{oldMediaId.Value}/");
                }
                else if (!string.IsNullOrEmpty(oldMedia.DuongDanLuuTru))
                {
                    await storageService.DeleteFileAsync(oldMedia.DuongDanLuuTru);
                }
                await mediaRepository.DeleteAssetAsync(oldMediaId.Value);
            }

            return Ok(new { message = "Đã xóa bài giảng thành công" });
        }

        // ── Upload Media (Bước 2.2) ───────────────────────────────

        /// <summary>Upload file (Video, PDF...) và gắn vào một bài giảng</summary>
        [HttpPost("lectures/{lectureId}/upload")]
        public async Task<IActionResult> UploadMedia(int lectureId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "Vui lòng chọn file để tải lên" });

            var lecture = await _courseRepository.GetLectureByIdAsync(lectureId);
            if (lecture == null)
                return NotFound(new { message = "Không tìm thấy bài giảng" });

            var userId = GetCurrentUserId();
            if (userId == null)
                return Unauthorized();

            // Xác định loại file
            var extension = Path.GetExtension(file.FileName).ToLower();
            var loaiTep = extension switch
            {
                ".mp4" or ".mov" or ".avi" or ".webm" => "Video",
                ".pdf" => "PDF",
                ".jpg" or ".jpeg" or ".png" or ".webp" => "Image",
                ".zip" or ".rar" => "ZipCode",
                _ => "Other",
            };

            // Lưu file vào thư mục uploads
            var uploadDir = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", "media");
            Directory.CreateDirectory(uploadDir);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadDir, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
                await file.CopyToAsync(stream);

            var relativePath = $"/uploads/media/{fileName}";

            // Tạo bản ghi TaiNguyenSo
            var media = new TaiNguyenSo
            {
                TaiLenBoi = userId.Value,
                TenTep = file.FileName,
                LoaiTep = loaiTep,
                KieuMIME = file.ContentType,
                DungLuongByte = file.Length,
                DuongDanLuuTru = relativePath,
                TrangThai = loaiTep == "Video" ? "Processing" : "Ready",
            };

            await _courseRepository.AddMediaAsync(media);

            // Gắn media vào bài giảng
            await _courseRepository.AttachMediaToLectureAsync(lectureId, media.MaTaiNguyen);

            return Ok(
                new MediaUploadResponse
                {
                    MaTaiNguyen = media.MaTaiNguyen,
                    TenTep = media.TenTep,
                    LoaiTep = media.LoaiTep,
                    DungLuongByte = media.DungLuongByte,
                    DuongDanLuuTru = media.DuongDanLuuTru,
                    TrangThai = media.TrangThai,
                    NgayTao = media.NgayTao,
                }
            );
        }

        // ── Admin/Kiểm duyệt ──────────────────────────────────────

        /// <summary>Xem toàn bộ cây giáo trình (Chương -> Bài giảng -> Tài nguyên) một lần</summary>
        [HttpGet("admin/courses/{courseId}/details")]
        [Authorize(Roles = "Admin,CMO,Moderator")]
        public async Task<IActionResult> GetCurriculumTree(Guid courseId)
        {
            var tree = await _courseRepository.GetFullCurriculumTreeAsync(courseId);

            var response = tree.Select(section => new CurriculumSectionNode
            {
                MaChuong = section.MaChuong,
                MaKhoaHoc = section.MaKhoaHoc,
                TieuDe = section.TieuDe,
                MoTa = section.MoTa,
                ThuTu = section.ThuTu,
                NgayTao = section.NgayTao,
                BaiGiangs = section
                    .BaiGiangs.Select(lecture => new CurriculumLectureNode
                    {
                        MaBaiGiang = lecture.MaBaiGiang,
                        MaChuong = lecture.MaChuong,
                        TieuDe = lecture.TieuDe,
                        LoaiBaiGiang = lecture.LoaiBaiGiang,
                        MoTa = lecture.MoTa,
                        ThuTu = lecture.ThuTu,
                        ThoiLuong = lecture.ThoiLuong,
                        XemMienPhi = lecture.XemMienPhi,
                        DangKhoa = lecture.DangKhoa,
                        MaTaiNguyen = lecture.MaTaiNguyen,
                        TrangThaiTaiNguyen = lecture.TaiNguyenSo?.TrangThai,
                        NgayTao = lecture.NgayTao,
                        TaiNguyenSo =
                            lecture.TaiNguyenSo != null
                                ? new MediaUploadResponse
                                {
                                    MaTaiNguyen = lecture.TaiNguyenSo.MaTaiNguyen,
                                    TenTep = lecture.TaiNguyenSo.TenTep,
                                    LoaiTep = lecture.TaiNguyenSo.LoaiTep,
                                    DungLuongByte = lecture.TaiNguyenSo.DungLuongByte,
                                    DuongDanLuuTru = lecture.TaiNguyenSo.DuongDanLuuTru,
                                    TrangThai = lecture.TaiNguyenSo.TrangThai,
                                    NgayTao = lecture.TaiNguyenSo.NgayTao,
                                }
                                : null,
                    })
                    .ToList(),
            });

            return Ok(response);
        }

        // ── Helpers ───────────────────────────────────────────────

        private Guid? GetCurrentUserId()
        {
            var claim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(claim, out var id) ? id : null;
        }

        private bool IsOwnerOrAdmin(KhoaHoc course)
        {
            if (User.IsInRole("Admin") || User.IsInRole("CMO") || User.IsInRole("Moderator"))
                return true;
            var userId = GetCurrentUserId();
            return userId.HasValue && course.MaGiangVien == userId.Value;
        }
    }
}
