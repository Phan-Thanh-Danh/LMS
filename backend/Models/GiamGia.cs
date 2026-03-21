using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("MaGiamGia")]
    public class GiamGia
    {
        [Key]
        [Column("MaGiamGia")]
        [MaxLength(100)]
        public string MaGiamGia { get; set; }
        public int? MaChienDich { get; set; }
        public Guid? MaGiangVien { get; set; }
        public int LoaiGiamGia { get; set; }
        public decimal GiaTriGiam { get; set; }
        public decimal? GiamToiDa { get; set; }
        public decimal DonHangToiThieu { get; set; } = 0;
        public int? GioiHanLuotDung { get; set; }
        public int SoLuotDaDung { get; set; } = 0;
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public bool DangHoatDong { get; set; } = true;
        public DateTime NgayTao { get; set; } = DateTime.Now;

        [ForeignKey("MaChienDich")]
        public virtual ChienDich? ChienDich { get; set; }

        [ForeignKey("MaGiangVien")]
        public virtual NguoiDung? GiangVien { get; set; }
    }
}
