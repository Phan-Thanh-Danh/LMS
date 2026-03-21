using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class TraLoiHoiDap
    {
        [Key]
        public int MaTraLoi { get; set; }
        public int MaChuDe { get; set; }
        public Guid MaNguoiDung { get; set; }

        [Required]
        public string NoiDungChiTiet { get; set; }
        public bool DuocGhimBoi { get; set; } = false;
        public int SoLuotUpvote { get; set; } = 0;
        public bool DaXoa { get; set; } = false;
        public DateTime NgayTao { get; set; } = DateTime.Now;

        [ForeignKey("MaChuDe")]
        public virtual ChuDeHoiDap ChuDe { get; set; }

        [ForeignKey("MaNguoiDung")]
        public virtual NguoiDung NguoiDung { get; set; }
    }
}
