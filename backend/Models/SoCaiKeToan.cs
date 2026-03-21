using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class SoCaiKeToan
    {
        [Key] public long MaBuToan { get; set; }
        [Required] [MaxLength(50)] public string MaDonHang { get; set; }
        public Guid MaGiangVien { get; set; }
        public Guid MaKhoaHoc { get; set; }
        public decimal DoanhThuGop { get; set; }
        public decimal PhiNenTang { get; set; }
        public decimal ThuNhapGiangVien { get; set; }
        public DateTime NgayGiaiPhong { get; set; }
        public bool DaGiaiPhong { get; set; } = false;
        public DateTime? GiaiPhongLuc { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;

        [ForeignKey("MaDonHang")] public virtual DonHang DonHang { get; set; }
        [ForeignKey("MaGiangVien")] public virtual NguoiDung GiangVien { get; set; }
        [ForeignKey("MaKhoaHoc")] public virtual KhoaHoc KhoaHoc { get; set; }
    }
}
