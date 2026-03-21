using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class MaHoaVideo
    {
        [Key]
        public int MaMaHoa { get; set; }
        public Guid MaTaiNguyen { get; set; }

        [Required]
        [MaxLength(20)]
        public string DoPhanGiai { get; set; }

        [Required]
        [MaxLength(1000)]
        public string DuongDanHLS { get; set; }
        public int? TocDoBitKbps { get; set; }
        public DateTime MaHoaLuc { get; set; } = DateTime.Now;

        [ForeignKey("MaTaiNguyen")]
        public virtual TaiNguyenSo TaiNguyenSo { get; set; }
    }
}
