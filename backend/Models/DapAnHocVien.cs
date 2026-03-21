using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class DapAnHocVien
    {
        [Key] public int MaDapAnChon { get; set; }
        public int MaLanLam { get; set; }
        public int MaCauHoi { get; set; }
        public int? MaLuaChonDaChon { get; set; }
        public bool LaDapAnDung { get; set; }

        [ForeignKey("MaLanLam")] public virtual LanLamBai LanLamBai { get; set; }
        [ForeignKey("MaCauHoi")] public virtual CauHoi CauHoi { get; set; }
        [ForeignKey("MaLuaChonDaChon")] public virtual LuaChonDapAn? LuaChonDapAn { get; set; }
    }
}
