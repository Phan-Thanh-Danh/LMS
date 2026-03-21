using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class MatHangGioHang
    {
        [Key] public int MaMatHang { get; set; }
        public int MaGioHang { get; set; }
        public Guid MaKhoaHoc { get; set; }
        public DateTime ThemVaoLuc { get; set; } = DateTime.Now;

        [ForeignKey("MaGioHang")] public virtual GioHang GioHang { get; set; }
        [ForeignKey("MaKhoaHoc")] public virtual KhoaHoc KhoaHoc { get; set; }
    }
}
