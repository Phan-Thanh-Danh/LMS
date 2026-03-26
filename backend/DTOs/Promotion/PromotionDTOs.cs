using System;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Promotion
{
    // Campaign DTOs
    public class CampaignResponse
    {
        public int MaChienDich { get; set; }
        public string TenChienDich { get; set; } = string.Empty;
        public string? MoTa { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string? DuongDanBanner { get; set; }
        public bool DangHoatDong { get; set; }
        public DateTime NgayTao { get; set; }
    }

    public class CreateCampaignRequest
    {
        [Required(ErrorMessage = "Tên chiến dịch không được để trống")]
        [MaxLength(300)]
        public string TenChienDich { get; set; } = string.Empty;
        public string? MoTa { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string? DuongDanBanner { get; set; }
    }

    // Coupon DTOs
    public class CouponResponse
    {
        public string MaGiamGia { get; set; } = string.Empty;
        public int? MaChienDich { get; set; }
        public string? TenChienDich { get; set; }
        public Guid? MaGiangVien { get; set; }
        public string? TenGiangVien { get; set; }
        public int LoaiGiamGia { get; set; } // 1-Percent, 2-Fixed
        public decimal GiaTriGiam { get; set; }
        public decimal? GiamToiDa { get; set; }
        public decimal DonHangToiThieu { get; set; }
        public int? GioiHanLuotDung { get; set; }
        public int SoLuotDaDung { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public bool DangHoatDong { get; set; }
    }

    public class CreateCouponRequest
    {
        [Required(ErrorMessage = "Mã giảm giá không được để trống")]
        [MaxLength(100)]
        public string MaGiamGia { get; set; } = string.Empty;
        public int? MaChienDich { get; set; }
        public Guid? MaGiangVien { get; set; }

        [Range(1, 2, ErrorMessage = "Loại giảm giá không hợp lệ (1-Phần trăm, 2-Số tiền cố định)")]
        public int LoaiGiamGia { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Giá trị giảm phải lớn hơn 0")]
        public decimal GiaTriGiam { get; set; }
        public decimal? GiamToiDa { get; set; }
        public decimal DonHangToiThieu { get; set; } = 0;
        public int? GioiHanLuotDung { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
    }

    public class BulkGenerateCouponRequest
    {
        [Required]
        public string Prefix { get; set; } = "COUPON";
        [Range(1, 1000)]
        public int Count { get; set; } = 10;
        
        // Cấu hình chung cho lô mã
        public int? MaChienDich { get; set; }
        public int LoaiGiamGia { get; set; }
        public decimal GiaTriGiam { get; set; }
        public decimal? GiamToiDa { get; set; }
        public decimal DonHangToiThieu { get; set; }
        public int? GioiHanLuotDung { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
    }
}
