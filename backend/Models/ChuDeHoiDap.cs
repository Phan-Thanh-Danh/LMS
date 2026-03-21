using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class ChuDeHoiDap
    {
        [Key]
        public int MaChuDe { get; set; }
        public int MaBaiGiang { get; set; }
        public Guid MaHocVien { get; set; }

        [Required]
        [MaxLength(500)]
        public string TieuDe { get; set; }

        [Required]
        public string NoiDungChiTiet { get; set; }
        public int SoLuotUpvote { get; set; } = 0;
        public bool DaGiaiDap { get; set; } = false;
        public bool DaXoa { get; set; } = false;
        public DateTime NgayTao { get; set; } = DateTime.Now;

        [ForeignKey("MaBaiGiang")]
        public virtual BaiGiang BaiGiang { get; set; }

        [ForeignKey("MaHocVien")]
        public virtual NguoiDung HocVien { get; set; }
    }
}
