using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class GhiDanh
    {
        [Key]
        public int MaGhiDanh { get; set; }
        public Guid MaHocVien { get; set; }
        public Guid MaKhoaHoc { get; set; }

        [MaxLength(50)]
        public string? MaDonHang { get; set; }
        public DateTime GhiDanhLuc { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(50)]
        public string TrangThaiTruyCap { get; set; } = "Active";
        public DateTime? ThuHoiLuc { get; set; }

        [ForeignKey("MaHocVien")]
        public virtual NguoiDung HocVien { get; set; }

        [ForeignKey("MaKhoaHoc")]
        public virtual KhoaHoc KhoaHoc { get; set; }

        [ForeignKey("MaDonHang")]
        public virtual DonHang? DonHang { get; set; }
    }
}
