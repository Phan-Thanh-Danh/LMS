using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class KhoaHoc
    {
        [Key]
        public Guid MaKhoaHoc { get; set; } = Guid.NewGuid();
        public Guid MaGiangVien { get; set; }
        public int MaDanhMuc { get; set; }

        [Required]
        [MaxLength(500)]
        public string TieuDe { get; set; }

        [Required]
        [MaxLength(500)]
        public string DuongDanURL { get; set; }

        [MaxLength(500)]
        public string? TieuDeNho { get; set; }
        public string? MoTa { get; set; }
        public string? MucTieuDauRa { get; set; }
        public string? YeuCauDieuKien { get; set; }

        [Required]
        [MaxLength(50)]
        public string TrinhDo { get; set; }

        [Required]
        [MaxLength(50)]
        public string NgonNgu { get; set; } = "Vietnamese";
        public decimal Gia { get; set; }

        [MaxLength(1000)]
        public string? DuongDanAnhDaiDien { get; set; }
        public Guid? MaVideoGioiThieu { get; set; }
        public int TongThoiLuong { get; set; } = 0;
        public int TongBaiGiang { get; set; } = 0;
        public decimal DiemDanhGiaTrungBinh { get; set; } = 0;
        public int TongDanhGia { get; set; } = 0;
        public int TongGhiDanh { get; set; } = 0;
        public int TrangThai { get; set; } = 0;
        public string? GhiChuTuChoi { get; set; }
        public DateTime? XuatBanLuc { get; set; }
        public DateTime? LuuTruLuc { get; set; }
        public bool DaXoa { get; set; } = false;
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public DateTime? NgayCapNhat { get; set; }

        [ForeignKey("MaGiangVien")]
        public virtual NguoiDung GiangVien { get; set; }

        [ForeignKey("MaDanhMuc")]
        public virtual DanhMuc DanhMuc { get; set; }

        [ForeignKey("MaVideoGioiThieu")]
        public virtual TaiNguyenSo? VideoGioiThieu { get; set; }
    }
}
