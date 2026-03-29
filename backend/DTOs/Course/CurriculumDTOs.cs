using System;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Course
{
    // ── Chương (Section) ──────────────────────────────────────────

    public class CreateSectionRequest
    {
        [Required(ErrorMessage = "Tiêu đề chương không được để trống")]
        [MaxLength(500)]
        public string TieuDe { get; set; }

        public string? MoTa { get; set; }

        [Range(0, int.MaxValue)]
        public int ThuTu { get; set; } = 0;
    }

    public class SectionResponse
    {
        public int MaChuong { get; set; }
        public Guid MaKhoaHoc { get; set; }
        public string TieuDe { get; set; }
        public string? MoTa { get; set; }
        public int ThuTu { get; set; }
        public DateTime NgayTao { get; set; }
    }

    // ── Bài giảng (Lecture) ───────────────────────────────────────

    public class CreateLectureRequest
    {
        [Required(ErrorMessage = "Tiêu đề bài giảng không được để trống")]
        [MaxLength(500)]
        public string TieuDe { get; set; }

        /// <summary>Video | Document | Quiz | Assignment</summary>
        [Required]
        [MaxLength(50)]
        public string LoaiBaiGiang { get; set; }

        public string? MoTa { get; set; }

        [Range(0, int.MaxValue)]
        public int ThuTu { get; set; } = 0;

        public bool XemMienPhi { get; set; } = false;

        /// <summary>ID tài nguyên số đã được upload (nếu có)</summary>
        public Guid? MaTaiNguyen { get; set; }
    }

    public class LectureResponse
    {
        public int MaBaiGiang { get; set; }
        public int MaChuong { get; set; }
        public string TieuDe { get; set; }
        public string LoaiBaiGiang { get; set; }
        public string? MoTa { get; set; }
        public int ThuTu { get; set; }
        public int? ThoiLuong { get; set; }
        public bool XemMienPhi { get; set; }
        public bool DangKhoa { get; set; }
        public Guid? MaTaiNguyen { get; set; }
        public string? TrangThaiTaiNguyen { get; set; }
        public DateTime NgayTao { get; set; }
    }

    public class CurriculumLectureNode : LectureResponse
    {
        public MediaUploadResponse? TaiNguyenSo { get; set; }
    }

    public class CurriculumSectionNode : SectionResponse
    {
        public List<CurriculumLectureNode> BaiGiangs { get; set; } = new();
    }

    // ── Media Upload ──────────────────────────────────────────────

    public class MediaUploadResponse
    {
        public Guid MaTaiNguyen { get; set; }
        public string TenTep { get; set; }
        public string LoaiTep { get; set; }
        public long DungLuongByte { get; set; }
        public string DuongDanLuuTru { get; set; }

        /// <summary>Pending | Processing | Ready | Failed</summary>
        public string TrangThai { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
