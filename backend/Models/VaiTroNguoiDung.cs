using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class VaiTroNguoiDung
    {
        [Key]
        public int MaVaiTroNguoiDung { get; set; }
        public Guid MaNguoiDung { get; set; }
        public int MaVaiTro { get; set; }
        public DateTime NgayGanVaiTro { get; set; } = DateTime.Now;
        public Guid? GanBoi { get; set; }

        [ForeignKey("MaNguoiDung")]
        public virtual NguoiDung NguoiDung { get; set; }

        [ForeignKey("MaVaiTro")]
        public virtual VaiTro VaiTro { get; set; }
    }
}
