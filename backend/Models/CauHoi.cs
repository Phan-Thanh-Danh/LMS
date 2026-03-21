using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class CauHoi
    {
        [Key]
        public int MaCauHoi { get; set; }
        public int MaBaiKiemTra { get; set; }

        [Required]
        public string NoiDungCauHoi { get; set; }

        [Required]
        [MaxLength(50)]
        public string LoaiCauHoi { get; set; }
        public string? GiaiThich { get; set; }
        public int ThuTu { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;

        [ForeignKey("MaBaiKiemTra")]
        public virtual BaiKiemTra BaiKiemTra { get; set; }
    }
}
