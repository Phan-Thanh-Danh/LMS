using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class NhatKyKiemToan
    {
        [Key]
        public long MaLog { get; set; }
        public Guid? MaNguoiDung { get; set; }

        [Required]
        [MaxLength(200)]
        public string HanhDong { get; set; }

        [MaxLength(100)]
        public string? LoaiDoiTuong { get; set; }

        [MaxLength(100)]
        public string? MaDoiTuong { get; set; }
        public string? GiaTriCu { get; set; }
        public string? GiaTriMoi { get; set; }

        [MaxLength(50)]
        public string? DiaChiIP { get; set; }

        [MaxLength(500)]
        public string? TrinhDuyet { get; set; }
        public DateTime ThoiDiem { get; set; } = DateTime.Now;
    }
}
