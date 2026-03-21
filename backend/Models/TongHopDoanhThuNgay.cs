using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class TongHopDoanhThuNgay
    {
        public DateTime NgayTongHop { get; set; }
        public Guid MaKhoaHoc { get; set; }
        public int TongDonHang { get; set; } = 0;
        public decimal TongDoanhThu { get; set; } = 0;
        public int TongHoanTien { get; set; } = 0;
        public decimal SoTienHoan { get; set; } = 0;

        [ForeignKey("MaKhoaHoc")] public virtual KhoaHoc KhoaHoc { get; set; }
    }
}
