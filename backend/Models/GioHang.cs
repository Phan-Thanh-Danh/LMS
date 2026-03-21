using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class GioHang
    {
        [Key] public int MaGioHang { get; set; }
        public Guid MaHocVien { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public DateTime? NgayCapNhat { get; set; }

        [ForeignKey("MaHocVien")] public virtual NguoiDung HocVien { get; set; }
    }
}
