using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class DonHang
    {
        [Key]
        [MaxLength(50)]
        public string MaDonHang { get; set; }
        public Guid MaHocVien { get; set; }

        [MaxLength(100)]
        public string? MaGiamGia { get; set; }
        public decimal SoTienGoc { get; set; }
        public decimal SoTienGiam { get; set; } = 0;
        public decimal TongTien { get; set; }

        [Required]
        [MaxLength(10)]
        public string DonViTienTe { get; set; } = "VND";

        [Required]
        [MaxLength(50)]
        public string CongThanhToan { get; set; }

        [MaxLength(200)]
        public string? MaGiaoDichCong { get; set; }

        [Required]
        [MaxLength(50)]
        public string TrangThaiThanhToan { get; set; } = "Pending";
        public DateTime? ThanhToanLuc { get; set; }
        public bool DaXoa { get; set; } = false;
        public DateTime NgayTao { get; set; } = DateTime.Now;

        [ForeignKey("MaHocVien")]
        public virtual NguoiDung HocVien { get; set; }

        [ForeignKey("MaGiamGia")]
        public virtual GiamGia? GiamGiaApDung { get; set; }
    }
}
