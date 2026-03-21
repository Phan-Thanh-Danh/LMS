using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class DanhGia
    {
        [Key] public int MaDanhGia { get; set; }
        public Guid MaKhoaHoc { get; set; }
        public Guid MaHocVien { get; set; }
        public int DiemSao { get; set; }
        public string? NhanXet { get; set; }
        public bool DaMuaKhoaHoc { get; set; } = true;
        public bool BiBaoCao { get; set; } = false;
        public bool DaXoa { get; set; } = false;
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public DateTime? NgayCapNhat { get; set; }

        [ForeignKey("MaKhoaHoc")] public virtual KhoaHoc KhoaHoc { get; set; }
        [ForeignKey("MaHocVien")] public virtual NguoiDung HocVien { get; set; }
    }
}
