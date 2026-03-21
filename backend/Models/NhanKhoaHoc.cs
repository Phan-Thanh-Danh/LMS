using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class NhanKhoaHoc
    {
        public Guid MaKhoaHoc { get; set; }
        public int MaNhan { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;

        [ForeignKey("MaKhoaHoc")] public virtual KhoaHoc KhoaHoc { get; set; }
        [ForeignKey("MaNhan")] public virtual NhanTuKhoa NhanTuKhoa { get; set; }
    }
}
