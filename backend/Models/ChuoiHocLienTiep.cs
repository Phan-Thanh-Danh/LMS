using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class ChuoiHocLienTiep
    {
        [Key] public int MaChuoi { get; set; }
        public Guid MaHocVien { get; set; }
        public int ChuoiHienTai { get; set; } = 0;
        public int ChuoiDaiNhat { get; set; } = 0;
        public DateTime? NgayHoatDongCuoi { get; set; }
        public int TheDoangBang { get; set; } = 0;
        public DateTime? NgayCapNhat { get; set; }

        [ForeignKey("MaHocVien")] public virtual NguoiDung HocVien { get; set; }
    }
}
