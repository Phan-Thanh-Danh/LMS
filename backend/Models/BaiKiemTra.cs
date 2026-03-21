using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class BaiKiemTra
    {
        [Key]
        public int MaBaiKiemTra { get; set; }
        public int MaBaiGiang { get; set; }

        [Required]
        [MaxLength(500)]
        public string TieuDe { get; set; }
        public decimal DiemDat { get; set; } = 70;
        public int? GioiHanThoiGianPhut { get; set; }
        public int? SoLanToiDa { get; set; }
        public bool XaoTronNgauNhien { get; set; } = true;
        public DateTime NgayTao { get; set; } = DateTime.Now;

        [ForeignKey("MaBaiGiang")]
        public virtual BaiGiang BaiGiang { get; set; }
    }
}
