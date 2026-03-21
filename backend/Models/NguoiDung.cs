using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class NguoiDung
    {
        [Key]
        public Guid MaNguoiDung { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [MaxLength(512)]
        public string MatKhauBam { get; set; }

        [Required]
        [MaxLength(256)]
        public string MuoiMatKhau { get; set; }

        [Required]
        [MaxLength(200)]
        public string HoTen { get; set; }

        [MaxLength(500)]
        public string? DuongDanAnhDaiDien { get; set; }
        public string? TieuSu { get; set; }
        public bool DangHoatDong { get; set; } = true;
        public bool DaXoa { get; set; } = false;
        public bool EmailDaXacThuc { get; set; } = false;
        public bool LaNhanVien { get; set; } = false;
        public DateTime? LanDangNhapCuoi { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public DateTime? NgayCapNhat { get; set; }
    }
}
