using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class ThongKeThoatBaiHoc
    {
        [Key]
        public int MaBaiGiang { get; set; }
        public int SoLuotXem { get; set; } = 0;
        public int SoLuotHoanThanh { get; set; } = 0;
        public int ThoiGianXemTrungBinhGiay { get; set; } = 0;
        public decimal TyLeThoat { get; set; } = 0;
        public DateTime CapNhatLanCuoi { get; set; }

        [ForeignKey("MaBaiGiang")]
        public virtual BaiGiang BaiGiang { get; set; }
    }
}
