using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class ThongBaoKhoaHoc
    {
        [Key]
        public int MaThongBao { get; set; }
        public Guid MaKhoaHoc { get; set; }
        public Guid MaGiangVien { get; set; }

        [Required]
        [MaxLength(500)]
        public string TieuDeThongBao { get; set; }

        [Required]
        public string NoiDung { get; set; }
        public DateTime GuiLuc { get; set; } = DateTime.Now;
        public int TongNguoiNhan { get; set; } = 0;

        [ForeignKey("MaKhoaHoc")]
        public virtual KhoaHoc KhoaHoc { get; set; }

        [ForeignKey("MaGiangVien")]
        public virtual NguoiDung GiangVien { get; set; }
    }
}
