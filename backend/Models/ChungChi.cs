using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class ChungChi
    {
        [Key]
        public Guid MaChungChi { get; set; } = Guid.NewGuid();
        public Guid MaHocVien { get; set; }
        public Guid MaKhoaHoc { get; set; }
        public DateTime NgayCapPhat { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(100)]
        public string MaXacThuc { get; set; }

        [MaxLength(1000)]
        public string? DuongDanPDFChungChi { get; set; }

        [ForeignKey("MaHocVien")]
        public virtual NguoiDung HocVien { get; set; }

        [ForeignKey("MaKhoaHoc")]
        public virtual KhoaHoc KhoaHoc { get; set; }
    }
}
