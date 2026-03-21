using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class HoanTien
    {
        [Key]
        public int MaHoanTien { get; set; }

        [Required]
        [MaxLength(50)]
        public string MaDonHang { get; set; }
        public Guid MaHocVien { get; set; }
        public string? LyDo { get; set; }
        public decimal SoTienHoan { get; set; }

        [Required]
        [MaxLength(50)]
        public string TrangThai { get; set; } = "Pending";
        public Guid? XuLyBoi { get; set; }
        public DateTime? XuLyLuc { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;

        [ForeignKey("MaDonHang")]
        public virtual DonHang DonHang { get; set; }

        [ForeignKey("MaHocVien")]
        public virtual NguoiDung HocVien { get; set; }

        [ForeignKey("XuLyBoi")]
        public virtual NguoiDung? NguoiDuyet { get; set; }
    }
}
