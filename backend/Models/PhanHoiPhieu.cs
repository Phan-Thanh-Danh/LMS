using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class PhanHoiPhieu
    {
        [Key]
        public int MaTraLoi { get; set; }

        [Required]
        [MaxLength(50)]
        public string MaPhieu { get; set; }
        public Guid MaNguoiGui { get; set; }

        [Required]
        public string NoiDungTinNhan { get; set; }
        public string? TepDinhKem { get; set; }
        public bool GhiChuNoiBo { get; set; } = false;
        public DateTime NgayTao { get; set; } = DateTime.Now;

        [ForeignKey("MaPhieu")]
        public virtual PhieuHoTro PhieuHoTro { get; set; }

        [ForeignKey("MaNguoiGui")]
        public virtual NguoiDung NguoiDung { get; set; }
    }
}
