using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class LanDangNhap
    {
        [Key]
        public long MaLanLam { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string? DiaChiIP { get; set; }

        [MaxLength(500)]
        public string? TrinhDuyet { get; set; }
        public bool DangNhapThanhCong { get; set; } = false;

        [MaxLength(200)]
        public string? LyDoThatBai { get; set; }
        public DateTime BatDauLamLuc { get; set; } = DateTime.Now;
    }
}
