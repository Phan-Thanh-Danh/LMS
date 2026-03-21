using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class NhanTuKhoa
    {
        [Key]
        public int MaNhan { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenNhan { get; set; }

        [Required]
        [MaxLength(100)]
        public string DuongDanURL { get; set; }
        public int SoLuotDung { get; set; } = 0;
    }
}
