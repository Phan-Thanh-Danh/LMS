using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class TaiNguyenSo
    {
        [Key]
        public Guid MaTaiNguyen { get; set; } = Guid.NewGuid();
        public Guid TaiLenBoi { get; set; }

        [Required]
        [MaxLength(500)]
        public string TenTep { get; set; }

        [Required]
        [MaxLength(50)]
        public string LoaiTep { get; set; }

        [Required]
        [MaxLength(100)]
        public string KieuMIME { get; set; }
        public long DungLuongByte { get; set; }

        [Required]
        [MaxLength(1000)]
        public string DuongDanLuuTru { get; set; }
        public int? ThoiLuong { get; set; }

        [Required]
        [MaxLength(50)]
        public string TrangThai { get; set; } = "Pending";
        public DateTime NgayTao { get; set; } = DateTime.Now;

        [ForeignKey("TaiLenBoi")]
        public virtual NguoiDung NguoiDung { get; set; }
    }
}
