using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class LuaChonDapAn
    {
        [Key]
        public int MaLuaChon { get; set; }
        public int MaCauHoi { get; set; }

        [Required]
        public string NoiDungLuaChon { get; set; }
        public bool LaDapAnDung { get; set; } = false;
        public int ThuTu { get; set; }

        [ForeignKey("MaCauHoi")]
        public virtual CauHoi CauHoi { get; set; }
    }
}
