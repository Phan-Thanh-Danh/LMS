using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class LichHoc
    {
        [Key]
        public int MaLichHoc { get; set; }
        public Guid MaHocVien { get; set; }
        public Guid MaKhoaHoc { get; set; }

        [Required]
        [MaxLength(50)]
        public string NgayTrongTuan { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }
        public int ThoiLuongToiThieuPhut { get; set; } = 30;
        public bool DangHoatDong { get; set; } = true;
        public DateTime NgayTao { get; set; } = DateTime.Now;

        [ForeignKey("MaHocVien")]
        public virtual NguoiDung HocVien { get; set; }

        [ForeignKey("MaKhoaHoc")]
        public virtual KhoaHoc KhoaHoc { get; set; }
    }
}
