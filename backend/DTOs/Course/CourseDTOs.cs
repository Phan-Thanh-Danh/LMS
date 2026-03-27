using System;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Course
{
    // ── Request DTOs ──────────────────────────────────────────────

    /// <summary>Bước 1.1 - Khởi tạo khóa học (chỉ cần thông tin tối thiểu)</summary>
    public class InitializeCourseRequest
    {
        [Required(ErrorMessage = "Tiêu đề khóa học không được để trống")]
        [MaxLength(500)]
        public string TieuDe { get; set; }

        [Required(ErrorMessage = "Danh mục không được để trống")]
        public int MaDanhMuc { get; set; }
    }

    /// <summary>Bước 1.2 - Cập nhật toàn bộ Metadata khóa học</summary>
    public class UpdateCourseMetadataRequest
    {
        [MaxLength(500)]
        public string? TieuDeNho { get; set; }

        public string? MoTa { get; set; }

        /// <summary>JSON array các mục tiêu đầu ra, VD: ["Hiểu được X","Biết cách làm Y"]</summary>
        public string? MucTieuDauRa { get; set; }

        /// <summary>JSON array điều kiện tiên quyết</summary>
        public string? YeuCauDieuKien { get; set; }

        /// <summary>Beginner | Intermediate | Advanced | AllLevels</summary>
        [MaxLength(50)]
        public string? TrinhDo { get; set; }

        [MaxLength(50)]
        public string? NgonNgu { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Giá không được âm")]
        public decimal? Gia { get; set; }

        [MaxLength(1000)]
        public string? DuongDanAnhDaiDien { get; set; }

        public int MaDanhMuc { get; set; }
    }

    // ── Response DTOs ─────────────────────────────────────────────

    public class CourseResponse
    {
        public Guid MaKhoaHoc { get; set; }
        public Guid MaGiangVien { get; set; }
        public string? TenGiangVien { get; set; }
        public int MaDanhMuc { get; set; }
        public string? TenDanhMuc { get; set; }
        public string TieuDe { get; set; } = string.Empty;
        public string DuongDanURL { get; set; } = string.Empty;
        public string? TieuDeNho { get; set; }
        public string? MoTa { get; set; }
        public string? MucTieuDauRa { get; set; }
        public string? YeuCauDieuKien { get; set; }
        public string TrinhDo { get; set; }
        public string NgonNgu { get; set; }
        public decimal Gia { get; set; }
        public string? DuongDanAnhDaiDien { get; set; }
        public int TongBaiGiang { get; set; }
        public int TongThoiLuong { get; set; }
        public decimal DiemDanhGiaTrungBinh { get; set; }
        public int TongGhiDanh { get; set; }

        /// <summary>0=Draft | 1=Pending | 2=Published | 3=Rejected | 4=Archived</summary>
        public int TrangThai { get; set; }
        public string TrangThaiText =>
            TrangThai switch
            {
                0 => "Bản nháp",
                1 => "Chờ duyệt",
                2 => "Đã xuất bản",
                3 => "Bị từ chối",
                4 => "Lưu trữ",
                _ => "Không xác định",
            };
        public string? GhiChuTuChoi { get; set; }
        public DateTime? XuatBanLuc { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
    }

    public class CourseListResponse
    {
        public Guid MaKhoaHoc { get; set; }
        public string TieuDe { get; set; } = string.Empty;
        public string DuongDanURL { get; set; } = string.Empty;
        public string? TieuDeNho { get; set; }
        public string? DuongDanAnhDaiDien { get; set; }
        public decimal Gia { get; set; }
        public string TrinhDo { get; set; } = string.Empty;
        public string? TenDanhMuc { get; set; }
        public string? TenGiangVien { get; set; }
        public decimal DiemDanhGiaTrungBinh { get; set; }
        public int TongGhiDanh { get; set; }
        public int TrangThai { get; set; }
        public string TrangThaiText =>
            TrangThai switch
            {
                0 => "Bản nháp",
                1 => "Chờ duyệt",
                2 => "Đã xuất bản",
                3 => "Bị từ chối",
                4 => "Lưu trữ",
                _ => "Không xác định",
            };
        public DateTime NgayTao { get; set; }
    }

    /// <summary>Yêu cầu từ chối khóa học (Kiểm duyệt viên)</summary>
    public class RejectCourseRequest
    {
        [Required(ErrorMessage = "Lý do từ chối không được để trống")]
        public string GhiChuTuChoi { get; set; } = string.Empty;
    }
}
