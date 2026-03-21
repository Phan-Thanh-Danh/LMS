using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class ChienDich
    {
        [Key]
        public int MaChienDich { get; set; }

        [Required]
        [MaxLength(300)]
        public string TenChienDich { get; set; }
        public string? MoTa { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }

        [MaxLength(1000)]
        public string? DuongDanBanner { get; set; }
        public bool DangHoatDong { get; set; } = true;
        public Guid TaoBoi { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;

        [ForeignKey("TaoBoi")]
        public virtual NguoiDung NguoiDung { get; set; }
    }
}
