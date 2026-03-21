using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class TuongTacNguoiDung
    {
        [Key]
        public long MaTuongTac { get; set; }
        public Guid MaNguoiDung { get; set; }
        public Guid MaKhoaHoc { get; set; }

        [Required]
        [MaxLength(50)]
        public string LoaiTuongTac { get; set; }
        public DateTime ThoiDiem { get; set; } = DateTime.Now;

        [MaxLength(100)]
        public string? MaPhien { get; set; }

        [ForeignKey("MaNguoiDung")]
        public virtual NguoiDung NguoiDung { get; set; }
    }
}
