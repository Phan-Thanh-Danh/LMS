using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class VaiTro
    {
        [Key]
        public int MaVaiTro { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenVaiTro { get; set; }

        [MaxLength(500)]
        public string? MoTa { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;
    }
}
