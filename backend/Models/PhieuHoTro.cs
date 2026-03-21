using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class PhieuHoTro
    {
        [Key]
        [MaxLength(50)]
        public string MaPhieu { get; set; }
        public Guid MaNguoiGuiYeuCau { get; set; }
        public Guid? PhanCongCho { get; set; }

        [Required]
        [MaxLength(100)]
        public string PhanLoai { get; set; }

        [Required]
        [MaxLength(500)]
        public string TieuDeThongBao { get; set; }

        [Required]
        [MaxLength(50)]
        public string MucUuTien { get; set; } = "Normal";

        [Required]
        [MaxLength(50)]
        public string TrangThai { get; set; } = "Open";

        [MaxLength(50)]
        public string? MaDonHangLienQuan { get; set; }
        public string? DuLieuBoiCanh { get; set; }
        public int? DiemHaiLong { get; set; }
        public DateTime? GiaiQuyetLuc { get; set; }
        public DateTime? DongPhieuLuc { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;

        [ForeignKey("MaNguoiGuiYeuCau")]
        public virtual NguoiDung NguoiGui { get; set; }

        [ForeignKey("PhanCongCho")]
        public virtual NguoiDung? NhanVienPhanCong { get; set; }
    }
}
