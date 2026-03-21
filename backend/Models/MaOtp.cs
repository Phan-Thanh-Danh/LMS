using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class MaOtp
    {
        [Key]
        public int MaToken { get; set; }
        public Guid MaNguoiDung { get; set; }

        [Required]
        [MaxLength(512)]
        public string GiaTriToken { get; set; }

        [Required]
        [MaxLength(50)]
        public string LoaiToken { get; set; }
        public DateTime HetHanLuc { get; set; }
        public bool DaSuDung { get; set; } = false;
        public DateTime NgayTao { get; set; } = DateTime.Now;

        [ForeignKey("MaNguoiDung")]
        public virtual NguoiDung NguoiDung { get; set; }
    }
}
