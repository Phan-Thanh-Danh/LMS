using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class DiemDanh
    {
        [Key]
        public long MaDiemDanh { get; set; }
        public int MaLichHoc { get; set; }
        public Guid MaHocVien { get; set; }
        public Guid MaKhoaHoc { get; set; }
        public DateTime NgayLichHoc { get; set; }
        public DateTime? DiemDanhLuc { get; set; }
        public int ThoiGianXemThucTeGiay { get; set; } = 0;
        public bool HopLe { get; set; } = false;

        [Required]
        [MaxLength(50)]
        public string TrangThai { get; set; }

        [ForeignKey("MaLichHoc")]
        public virtual LichHoc LichHoc { get; set; }

        [ForeignKey("MaHocVien")]
        public virtual NguoiDung HocVien { get; set; }

        [ForeignKey("MaKhoaHoc")]
        public virtual KhoaHoc KhoaHoc { get; set; }
    }
}
