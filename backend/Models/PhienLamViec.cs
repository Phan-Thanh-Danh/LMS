using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class PhienLamViec
    {
        [Key]
        public Guid MaPhien { get; set; } = Guid.NewGuid();
        public Guid MaNguoiDung { get; set; }

        [Required]
        [MaxLength(512)]
        public string TokenLamMoi { get; set; }

        [MaxLength(50)]
        public string? DiaChiIP { get; set; }

        [MaxLength(500)]
        public string? ThongTinThietBi { get; set; }

        [MaxLength(200)]
        public string? ViTri { get; set; }
        public DateTime HetHanLuc { get; set; }
        public bool DaThuHoi { get; set; } = false;
        public DateTime NgayTao { get; set; } = DateTime.Now;

        [ForeignKey("MaNguoiDung")]
        public virtual NguoiDung NguoiDung { get; set; }
    }
}
