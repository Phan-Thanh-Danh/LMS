using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class DanhMuc
    {
        [Key]
        public int MaDanhMuc { get; set; }
        public int? MaDanhMucCha { get; set; }

        [Required]
        [MaxLength(200)]
        public string TenDanhMuc { get; set; }

        [Required]
        [MaxLength(200)]
        public string DuongDanURL { get; set; }
        public int ThuTuHienThi { get; set; } = 0;
        public bool DangHienThi { get; set; } = true;

        [MaxLength(500)]
        public string? DuongDanIcon { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;

        [ForeignKey("MaDanhMucCha")]
        public virtual DanhMuc? DanhMucCha { get; set; }
    }
}
