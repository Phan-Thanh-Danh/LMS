using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class ChiTietDonHang
    {
        [Key]
        public int MaChiTietDonHang { get; set; }

        [Required]
        [MaxLength(50)]
        public string MaDonHang { get; set; }
        public Guid MaKhoaHoc { get; set; }
        public decimal GiaTaiThoiDiemMua { get; set; }
        public decimal GiamGiaApDung { get; set; } = 0;

        [ForeignKey("MaDonHang")]
        public virtual DonHang DonHang { get; set; }

        [ForeignKey("MaKhoaHoc")]
        public virtual KhoaHoc KhoaHoc { get; set; }
    }
}
