using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Chuong
    {
        [Key]
        public int MaChuong { get; set; }
        public Guid MaKhoaHoc { get; set; }

        [Required]
        [MaxLength(500)]
        public string TieuDe { get; set; }
        public string? MoTa { get; set; }
        public int ThuTu { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;

        [ForeignKey("MaKhoaHoc")]
        public virtual KhoaHoc KhoaHoc { get; set; }
    }
}
