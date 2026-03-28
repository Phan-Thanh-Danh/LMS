using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using backend.DTOs.Course;
using backend.Models;
using backend.Repository.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IEmailService _emailService;

        public CoursesController(
            ICourseRepository courseRepository,
            IWebHostEnvironment env,
            IEmailService emailService
        )
        {
            _courseRepository = courseRepository;
            _env = env;
            _emailService = emailService;
        }

        // ── Danh sách công khai ────────────────────────────────────

        /// <summary>Lấy tất cả khóa học đã xuất bản (Public)</summary>
        [HttpGet]
        public async Task<IActionResult> GetPublishedCourses()
        {
            var courses = await _courseRepository.GetPublishedCoursesAsync();
            var response = courses.Select(MapToListResponse);
            return Ok(response);
        }

        /// <summary>Xem chi tiết một khóa học công khai (Public)</summary>
        [HttpGet("{idOrSlug}")]
        public async Task<IActionResult> GetCourse(string idOrSlug)
        {
            KhoaHoc? course;
            if (Guid.TryParse(idOrSlug, out var guid))
                course = await _courseRepository.GetCourseByIdAsync(guid);
            else
                course = await _courseRepository.GetCourseBySlugAsync(idOrSlug);

            if (course == null)
                return NotFound(new { message = "Không tìm thấy khóa học" });
            return Ok(MapToResponse(course));
        }

        // ── Giảng viên quản lý khóa học của mình ──────────────────

        /// <summary>Giảng viên xem danh sách khóa học của mình</summary>
        [Authorize(Roles = "Instructor")]
        [HttpGet("my-courses")]
        public async Task<IActionResult> GetMyCourses()
        {
            var userId = GetCurrentUserId();
            if (userId == null)
                return Unauthorized();

            var courses = await _courseRepository.GetCoursesByInstructorAsync(userId.Value);
            return Ok(courses.Select(MapToListResponse));
        }

        /// <summary>Bước 1.1 - Giảng viên khởi tạo khóa học Draft</summary>
        [Authorize(Roles = "Instructor")]
        [HttpPost]
        public async Task<IActionResult> InitializeCourse(
            [FromBody] InitializeCourseRequest request
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetCurrentUserId();
            if (userId == null)
                return Unauthorized();

            var slug = GenerateSlug(request.TieuDe);
            if (!await _courseRepository.IsSlugUniqueAsync(slug))
                slug = $"{slug}-{Guid.NewGuid().ToString("N").Substring(0, 8)}";

            var course = new KhoaHoc
            {
                MaGiangVien = userId.Value,
                MaDanhMuc = request.MaDanhMuc,
                TieuDe = request.TieuDe,
                DuongDanURL = slug,
                TrinhDo = "AllLevels",
                NgonNgu = "Vietnamese",
                Gia = 0,
                TrangThai = 0, // Draft
            };

            await _courseRepository.AddCourseAsync(course);
            return CreatedAtAction(
                nameof(GetCourse),
                new { idOrSlug = course.MaKhoaHoc.ToString() },
                new
                {
                    message = "Khởi tạo khóa học thành công",
                    maKhoaHoc = course.MaKhoaHoc,
                    slug = course.DuongDanURL,
                }
            );
        }

        /// <summary>Bước 1.2 - Giảng viên cập nhật thông tin chung và giá</summary>
        [Authorize(Roles = "Instructor")]
        [HttpPut("{id}/metadata")]
        public async Task<IActionResult> UpdateMetadata(
            Guid id,
            [FromBody] UpdateCourseMetadataRequest request
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var course = await _courseRepository.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound(new { message = "Không tìm thấy khóa học" });

            var userId = GetCurrentUserId();
            if (course.MaGiangVien != userId)
                return Forbid();

            // Chỉ cho sửa khi đang ở Draft hoặc Rejected
            if (course.TrangThai == 1 || course.TrangThai == 2)
                return BadRequest(
                    new
                    {
                        message = "Không thể chỉnh sửa khóa học đang trong trạng thái Chờ duyệt hoặc Đã xuất bản",
                    }
                );

            if (request.TieuDeNho != null)
                course.TieuDeNho = request.TieuDeNho;
            if (request.MoTa != null)
                course.MoTa = request.MoTa;
            if (request.MucTieuDauRa != null)
                course.MucTieuDauRa = request.MucTieuDauRa;
            if (request.YeuCauDieuKien != null)
                course.YeuCauDieuKien = request.YeuCauDieuKien;
            if (request.TrinhDo != null)
                course.TrinhDo = request.TrinhDo;
            if (request.NgonNgu != null)
                course.NgonNgu = request.NgonNgu;
            if (request.Gia.HasValue)
                course.Gia = request.Gia.Value;
            if (request.DuongDanAnhDaiDien != null)
                course.DuongDanAnhDaiDien = request.DuongDanAnhDaiDien;
            if (request.MaDanhMuc > 0)
                course.MaDanhMuc = request.MaDanhMuc;

            await _courseRepository.UpdateCourseAsync(course);
            return Ok(new { message = "Cập nhật thông tin khóa học thành công" });
        }

        /// <summary>Bước 3.1 - Giảng viên gửi yêu cầu phê duyệt</summary>
        [Authorize(Roles = "Instructor")]
        [HttpPost("{id}/submit")]
        public async Task<IActionResult> SubmitForReview(Guid id)
        {
            var course = await _courseRepository.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound(new { message = "Không tìm thấy khóa học" });

            var userId = GetCurrentUserId();
            if (course.MaGiangVien != userId)
                return Forbid();

            if (course.TrangThai != 0 && course.TrangThai != 3)
                return BadRequest(
                    new
                    {
                        message = "Chỉ có thể gửi phê duyệt khi khóa học ở trạng thái Bản nháp hoặc Bị từ chối",
                    }
                );

            var success = await _courseRepository.SubmitForReviewAsync(id);
            if (!success)
                return BadRequest(new { message = "Không thể gửi yêu cầu phê duyệt" });

            return Ok(
                new
                {
                    message = "Đã gửi yêu cầu phê duyệt thành công. Khóa học đang chờ kiểm duyệt.",
                }
            );
        }

        /// <summary>Giảng viên xóa mềm khóa học (chỉ khi Draft)</summary>
        [Authorize(Roles = "Instructor")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var course = await _courseRepository.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound(new { message = "Không tìm thấy khóa học" });

            var userId = GetCurrentUserId();
            if (course.MaGiangVien != userId)
                return Forbid();

            if (course.TrangThai != 0)
                return BadRequest(
                    new { message = "Chỉ có thể xóa khóa học ở trạng thái Bản nháp" }
                );

            await _courseRepository.DeleteCourseAsync(id);
            return Ok(new { message = "Đã xóa khóa học thành công" });
        }

        // ── Kiểm duyệt viên (Admin/CMO) ───────────────────────────

        /// <summary>Danh sách khóa học chờ duyệt</summary>
        [Authorize(Roles = "Admin,CMO,Moderator")]
        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingCourses()
        {
            var courses = await _courseRepository.GetPendingCoursesAsync();
            return Ok(courses.Select(MapToListResponse));
        }

        /// <summary>Bước 3.2 - Kiểm duyệt viên phê duyệt khóa học</summary>
        [Authorize(Roles = "Admin,CMO,Moderator")]
        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApproveCourse(Guid id)
        {
            var success = await _courseRepository.ApproveCourseAsync(id);
            if (!success)
                return BadRequest(
                    new
                    {
                        message = "Không thể phê duyệt. Khóa học phải đang ở trạng thái Chờ duyệt.",
                    }
                );
            return Ok(new { message = "Phê duyệt thành công. Khóa học đã được xuất bản." });
        }

        /// <summary>Bước 3.2 - Kiểm duyệt viên từ chối khóa học</summary>
        [Authorize(Roles = "Admin,CMO,Moderator")]
        [HttpPost("{id}/reject")]
        public async Task<IActionResult> RejectCourse(
            Guid id,
            [FromBody] RejectCourseRequest request
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var success = await _courseRepository.RejectCourseAsync(id, request.GhiChuTuChoi);
            if (!success)
                return BadRequest(
                    new
                    {
                        message = "Không thể từ chối. Khóa học phải đang ở trạng thái Chờ duyệt.",
                    }
                );
            return Ok(new { message = "Đã từ chối và trả lại cho Giảng viên chỉnh sửa." });
        }

        /// <summary>Admin lưu trữ khóa học</summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/archive")]
        public async Task<IActionResult> ArchiveCourse(Guid id)
        {
            var success = await _courseRepository.ArchiveCourseAsync(id);
            if (!success)
                return NotFound(new { message = "Không tìm thấy khóa học" });
            return Ok(new { message = "Đã lưu trữ khóa học" });
        }

        /// <summary>Vô hiệu hóa/Kích hoạt lại khóa học (Toggle Status)</summary>
        [Authorize(Roles = "Admin,CMO,Moderator,Instructor")]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ToggleCourseStatus(
            Guid id,
            [FromBody] ModerationActionRequest? request = null
        )
        {
            var course = await _courseRepository.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound(new { message = "Không tìm thấy khóa học" });

            var currentUserId = GetCurrentUserId();
            bool isOwner = currentUserId != null && course.MaGiangVien == currentUserId;

            // Nếu người gọi là Giảng viên nhưng không phải chủ sở hữu -> cấm
            if (User.IsInRole("Instructor") && !isOwner)
            {
                // Ngoại trừ trường hợp họ vừa có quyền Instructor vừa có quyền Admin/Moderator
                if (!User.IsInRole("Admin") && !User.IsInRole("Moderator") && !User.IsInRole("CMO"))
                    return Forbid();
            }

            // Lưu trạng thái trước đó để kiểm tra việc ẩn
            bool wasPublished = course.TrangThai == 2;

            var success = await _courseRepository.ToggleCourseStatusAsync(id);
            if (!success)
                return NotFound(new { message = "Không tìm thấy khóa học" });

            // Nếu khóa học bị vô hiệu hóa bởi Admin/Kiểm duyệt (không phải chủ sở hữu làm)
            if (wasPublished && !isOwner && course.GiangVien != null)
            {
                var reasonText = string.IsNullOrWhiteSpace(request?.LyDo)
                    ? "Vi phạm chính sách nội dung hoặc bản quyền"
                    : request.LyDo;

                var body =
                    $@"
                    <h3>Thông báo hệ thống</h3>
                    <p>Chào {course.GiangVien.HoTen},</p>
                    <p>Khóa học <strong>{course.TieuDe}</strong> của bạn đã bị vô hiệu hóa/ẩn khỏi danh sách hiển thị.</p>
                    <p><strong>Lý do:</strong> {reasonText}</p>
                    <p>Vui lòng liên hệ với bộ phận hỗ trợ để biết thêm chi tiết.</p>";

                await _emailService.SendEmailAsync(
                    course.GiangVien.Email,
                    "[LMS] Khóa học của bạn đã bị vô hiệu hóa",
                    body
                );
            }

            return Ok(new { message = "Đã thay đổi trạng thái khóa học" });
        }

        /// <summary>Xóa cứng khóa học</summary>
        [Authorize(Roles = "Admin,Moderator,Instructor")]
        [HttpDelete("{id}/hard")]
        public async Task<IActionResult> HardDeleteCourse(
            Guid id,
            [FromBody] ModerationActionRequest? request = null
        )
        {
            var course = await _courseRepository.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound(new { message = "Không tìm thấy khóa học" });

            var currentUserId = GetCurrentUserId();
            bool isOwner = currentUserId != null && course.MaGiangVien == currentUserId;

            if (User.IsInRole("Instructor") && !isOwner)
            {
                if (!User.IsInRole("Admin") && !User.IsInRole("Moderator"))
                    return Forbid();
            }

            var hasEnrollments = await _courseRepository.HasEnrollmentsAsync(id);
            if (hasEnrollments)
                return BadRequest(
                    new
                    {
                        message = "Không thể xóa cứng khóa học đã có học viên đăng ký. Vui lòng chuyển sang trạng thái Lưu trữ (Archive) thay vì xóa cứng.",
                    }
                );

            var webRootPath = _env.WebRootPath ?? "wwwroot";
            var success = await _courseRepository.HardDeleteCourseAsync(id, webRootPath);

            if (!success)
                return BadRequest(
                    new
                    {
                        message = "Không thể xóa cứng khóa học này. Có thể do lỗi dữ liệu kết nối.",
                    }
                );

            // Gửi email báo cho Giảng viên nếu Admin/Kiểm duyệt viên tự ý xóa
            if (!isOwner && course.GiangVien != null)
            {
                var reasonText = string.IsNullOrWhiteSpace(request?.LyDo)
                    ? "Vi phạm nghiêm trọng chính sách nội dung và quy định của nền tảng"
                    : request.LyDo;

                var body =
                    $@"
                    <h3>Thông báo hệ thống</h3>
                    <p>Chào {course.GiangVien.HoTen},</p>
                    <p>Khóa học <strong>{course.TieuDe}</strong> của bạn đã bị bộ phận Quản trị/Kiểm duyệt xóa vĩnh viễn khỏi hệ thống.</p>
                    <p><strong>Lý do:</strong> {reasonText}</p>
                    <p>Mọi thắc mắc vui lòng liên hệ bộ phận hỗ trợ.</p>";

                await _emailService.SendEmailAsync(
                    course.GiangVien.Email,
                    "[LMS] Khóa học của bạn đã bị xóa vĩnh viễn",
                    body
                );
            }

            return Ok(
                new { message = "Đã xóa cứng khóa học và toàn bộ học liệu liên quan vĩnh viễn" }
            );
        }

        // ── Helpers ───────────────────────────────────────────────

        private Guid? GetCurrentUserId()
        {
            var claim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(claim, out var id) ? id : null;
        }

        private static string GenerateSlug(string title)
        {
            var slug = title
                .ToLower()
                .Replace("đ", "d")
                .Normalize(System.Text.NormalizationForm.FormD);
            slug = new string(slug.Where(c => c < 128).ToArray());
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
            slug = Regex.Replace(slug, @"\s+", "-");
            slug = slug.Trim('-');
            return slug.Length > 100 ? slug.Substring(0, 100) : slug;
        }

        private CourseResponse MapToResponse(KhoaHoc k) =>
            new()
            {
                MaKhoaHoc = k.MaKhoaHoc,
                MaGiangVien = k.MaGiangVien,
                TenGiangVien = k.GiangVien?.HoTen,
                MaDanhMuc = k.MaDanhMuc,
                TenDanhMuc = k.DanhMuc?.TenDanhMuc,
                TieuDe = k.TieuDe,
                DuongDanURL = k.DuongDanURL,
                TieuDeNho = k.TieuDeNho,
                MoTa = k.MoTa,
                MucTieuDauRa = k.MucTieuDauRa,
                YeuCauDieuKien = k.YeuCauDieuKien,
                TrinhDo = k.TrinhDo,
                NgonNgu = k.NgonNgu,
                Gia = k.Gia,
                DuongDanAnhDaiDien = k.DuongDanAnhDaiDien,
                TongBaiGiang = k.TongBaiGiang,
                TongThoiLuong = k.TongThoiLuong,
                DiemDanhGiaTrungBinh = k.DiemDanhGiaTrungBinh,
                TongGhiDanh = k.TongGhiDanh,
                TrangThai = k.TrangThai,
                GhiChuTuChoi = k.GhiChuTuChoi,
                XuatBanLuc = k.XuatBanLuc,
                NgayTao = k.NgayTao,
                NgayCapNhat = k.NgayCapNhat,
            };

        private CourseListResponse MapToListResponse(KhoaHoc k) =>
            new()
            {
                MaKhoaHoc = k.MaKhoaHoc,
                TieuDe = k.TieuDe,
                DuongDanURL = k.DuongDanURL,
                TieuDeNho = k.TieuDeNho,
                DuongDanAnhDaiDien = k.DuongDanAnhDaiDien,
                Gia = k.Gia,
                TrinhDo = k.TrinhDo,
                TenDanhMuc = k.DanhMuc?.TenDanhMuc,
                TenGiangVien = k.GiangVien?.HoTen,
                DiemDanhGiaTrungBinh = k.DiemDanhGiaTrungBinh,
                TongGhiDanh = k.TongGhiDanh,
                TrangThai = k.TrangThai,
                NgayTao = k.NgayTao,
            };
    }
}
