using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class TienDoHocTap
    {
        [Key]
        public long MaTienDo { get; set; }
        public int MaGhiDanh { get; set; }
        public int MaBaiGiang { get; set; }
        public int ThoiGianXemGiay { get; set; } = 0;
        public int ViTriXemCuoi { get; set; } = 0;

        [Required]
        [MaxLength(50)]
        public string TrangThai { get; set; } = "NotStarted";
        public DateTime? HoanThanhLuc { get; set; }
        public DateTime? NgayCapNhat { get; set; }

        [ForeignKey("MaGhiDanh")]
        public virtual GhiDanh GhiDanh { get; set; }

        [ForeignKey("MaBaiGiang")]
        public virtual BaiGiang BaiGiang { get; set; }
    }
}
