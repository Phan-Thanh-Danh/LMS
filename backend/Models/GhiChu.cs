using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class GhiChu
    {
        [Key]
        public int MaGhiChu { get; set; }
        public Guid MaHocVien { get; set; }
        public int MaBaiGiang { get; set; }
        public int ThoiDiem { get; set; }

        [Required]
        public string NoiDung { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public DateTime? NgayCapNhat { get; set; }

        [ForeignKey("MaHocVien")]
        public virtual NguoiDung HocVien { get; set; }

        [ForeignKey("MaBaiGiang")]
        public virtual BaiGiang BaiGiang { get; set; }
    }
}
