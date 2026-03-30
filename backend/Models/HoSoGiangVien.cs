using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class HoSoGiangVien
    {
        [Key]
        public Guid MaGiangVien { get; set; }

        [MaxLength(300)]
        public string? DanhHieu { get; set; }
        public string? TieuSuChiTiet { get; set; }

        [MaxLength(500)]
        public string? DuongDanWebsite { get; set; }

        [MaxLength(500)]
        public string? DuongDanLinkedIn { get; set; }
        public string? ThongTinNganHangMaHoa { get; set; }
        public string? MaSoThueMaHoa { get; set; }
        public decimal TyLeDoanhThu { get; set; } = 70;

        [Required]
        [MaxLength(50)]
        public string TrangThaiKYC { get; set; } = "Pending";
        public Guid? MaTaiNguyenCCCDMatTruoc { get; set; }
        public Guid? MaTaiNguyenCCCDMatSau { get; set; }
        public string? DanhSachMaTaiNguyenBangCap { get; set; }
        public string? LyDoTuChoi { get; set; }
        public DateTime? KyKetDieuKhoanLuc { get; set; }
        public DateTime? NgayCapNhat { get; set; }

        [ForeignKey("MaGiangVien")]
        public virtual NguoiDung NguoiDung { get; set; }
    }
}
