using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class LanLamBai
    {
        [Key] public int MaLanLam { get; set; }
        public Guid MaHocVien { get; set; }
        public int MaBaiKiemTra { get; set; }
        public decimal DiemSo { get; set; }
        public int TongCauHoi { get; set; }
        public int SoCauDung { get; set; }
        public bool DaDat { get; set; }
        public int? ThoiGianLamGiay { get; set; }
        public DateTime BatDauLamLuc { get; set; } = DateTime.Now;
        public DateTime? NopBaiLuc { get; set; }

        [ForeignKey("MaHocVien")] public virtual NguoiDung HocVien { get; set; }
        [ForeignKey("MaBaiKiemTra")] public virtual BaiKiemTra BaiKiemTra { get; set; }
    }
}
