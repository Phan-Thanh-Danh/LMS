using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class HoSoNhanVien
    {
        [Key]
        public Guid MaNhanVien { get; set; }

        [MaxLength(50)]
        public string? MaNhanVienNoiBo { get; set; }

        [MaxLength(200)]
        public string? PhongBan { get; set; }

        [MaxLength(200)]
        public string? ChucDanh { get; set; }

        [MaxLength(20)]
        public string? SoDienThoaiNoiBo { get; set; }
        public string? QuyenBoSung { get; set; }
        public DateTime? NgayVaoLam { get; set; }
        public bool DangHoatDong { get; set; } = true;
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public DateTime? NgayCapNhat { get; set; }

        [ForeignKey("MaNhanVien")]
        public virtual NguoiDung NguoiDung { get; set; }
    }
}
