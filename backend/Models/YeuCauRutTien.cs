using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class YeuCauRutTien
    {
        [Key]
        public int MaYeuCau { get; set; }
        public Guid MaGiangVien { get; set; }
        public decimal SoTienYeuCau { get; set; }

        [Required]
        [MaxLength(50)]
        public string TrangThai { get; set; } = "Pending";

        [Required]
        public string SnapshotNganHang { get; set; }
        public Guid? DuyetBoi { get; set; }
        public string? GhiChuDuyet { get; set; }
        public DateTime? ThanhToanLuc { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;

        [ForeignKey("MaGiangVien")]
        public virtual NguoiDung GiangVien { get; set; }

        [ForeignKey("DuyetBoi")]
        public virtual NguoiDung? NguoiDuyet { get; set; }
    }
}
