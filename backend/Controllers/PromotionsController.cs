using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.DTOs.Promotion;
using backend.Models;
using backend.Repository.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/v1/promotions")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly IPromotionRepository _promotionRepository;

        public PromotionsController(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        #region Campaigns (CMO/Admin Only)

        [HttpGet("campaigns")]
        public async Task<ActionResult<IEnumerable<CampaignResponse>>> GetCampaigns()
        {
            var campaigns = await _promotionRepository.GetCampaignsAsync();
            var response = campaigns.Select(c => new CampaignResponse
            {
                MaChienDich = c.MaChienDich,
                TenChienDich = c.TenChienDich,
                MoTa = c.MoTa,
                NgayBatDau = c.NgayBatDau,
                NgayKetThuc = c.NgayKetThuc,
                DuongDanBanner = c.DuongDanBanner,
                DangHoatDong = c.DangHoatDong,
                NgayTao = c.NgayTao,
            });
            return Ok(response);
        }

        [HttpGet("campaigns/{id}")]
        public async Task<ActionResult<CampaignResponse>> GetCampaign(int id)
        {
            var c = await _promotionRepository.GetCampaignByIdAsync(id);
            if (c == null)
                return NotFound(new { message = "Không tìm thấy chiến dịch" });

            return Ok(
                new CampaignResponse
                {
                    MaChienDich = c.MaChienDich,
                    TenChienDich = c.TenChienDich,
                    MoTa = c.MoTa,
                    NgayBatDau = c.NgayBatDau,
                    NgayKetThuc = c.NgayKetThuc,
                    DuongDanBanner = c.DuongDanBanner,
                    DangHoatDong = c.DangHoatDong,
                    NgayTao = c.NgayTao,
                }
            );
        }

        [Authorize(Roles = "Admin,CMO")]
        [HttpPost("campaigns")]
        public async Task<ActionResult<CampaignResponse>> CreateCampaign(
            [FromBody] CreateCampaignRequest request
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request.NgayKetThuc <= request.NgayBatDau)
                return BadRequest(new { message = "Ngày kết thúc phải lớn hơn ngày bắt đầu" });

            var userIdClaim = User.FindFirst(
                System.Security.Claims.ClaimTypes.NameIdentifier
            )?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            var campaign = new ChienDich
            {
                TenChienDich = request.TenChienDich,
                MoTa = request.MoTa,
                NgayBatDau = request.NgayBatDau,
                NgayKetThuc = request.NgayKetThuc,
                DuongDanBanner = request.DuongDanBanner,
                DangHoatDong = true,
                TaoBoi = Guid.Parse(userIdClaim),
            };

            await _promotionRepository.AddCampaignAsync(campaign);

            return CreatedAtAction(
                nameof(GetCampaign),
                new { id = campaign.MaChienDich },
                new CampaignResponse
                {
                    MaChienDich = campaign.MaChienDich,
                    TenChienDich = campaign.TenChienDich,
                    NgayTao = campaign.NgayTao,
                }
            );
        }

        [Authorize(Roles = "Admin,CMO")]
        [HttpPut("campaigns/{id}")]
        public async Task<IActionResult> UpdateCampaign(
            int id,
            [FromBody] CreateCampaignRequest request
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var campaign = await _promotionRepository.GetCampaignByIdAsync(id);
            if (campaign == null)
                return NotFound(new { message = "Không tìm thấy chiến dịch" });

            campaign.TenChienDich = request.TenChienDich;
            campaign.MoTa = request.MoTa;
            campaign.NgayBatDau = request.NgayBatDau;
            campaign.NgayKetThuc = request.NgayKetThuc;
            campaign.DuongDanBanner = request.DuongDanBanner;

            await _promotionRepository.UpdateCampaignAsync(campaign);
            return Ok(new { message = "Cập nhật chiến dịch thành công" });
        }

        [Authorize(Roles = "Admin,CMO")]
        [HttpPatch("campaigns/{id}/status")]
        public async Task<IActionResult> ToggleCampaignStatus(int id)
        {
            var success = await _promotionRepository.ToggleCampaignStatusAsync(id);
            if (!success)
                return NotFound(new { message = "Không tìm thấy chiến dịch" });

            return Ok(new { message = "Đã cập nhật trạng thái hoạt động của chiến dịch" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("campaigns/{id}")]
        public async Task<IActionResult> DeleteCampaign(int id)
        {
            var success = await _promotionRepository.DeleteCampaignAsync(id);
            if (!success)
                return NotFound(new { message = "Không tìm thấy chiến dịch" });

            return Ok(new { message = "Xóa cứng chiến dịch thành công" });
        }

        #endregion

        #region Coupons

        [HttpGet("coupons")]
        public async Task<ActionResult<IEnumerable<CouponResponse>>> GetCoupons(
            [FromQuery] int? campaignId,
            [FromQuery] Guid? instructorId
        )
        {
            var coupons = await _promotionRepository.GetCouponsAsync(campaignId, instructorId);
            var response = coupons.Select(g => new CouponResponse
            {
                MaGiamGia = g.MaGiamGia,
                MaChienDich = g.MaChienDich,
                TenChienDich = g.ChienDich?.TenChienDich,
                MaGiangVien = g.MaGiangVien,
                TenGiangVien = g.GiangVien?.HoTen,
                LoaiGiamGia = g.LoaiGiamGia,
                GiaTriGiam = g.GiaTriGiam,
                GiamToiDa = g.GiamToiDa,
                DonHangToiThieu = g.DonHangToiThieu,
                GioiHanLuotDung = g.GioiHanLuotDung,
                SoLuotDaDung = g.SoLuotDaDung,
                NgayBatDau = g.NgayBatDau,
                NgayKetThuc = g.NgayKetThuc,
                DangHoatDong = g.DangHoatDong,
            });
            return Ok(response);
        }

        [HttpGet("coupons/{code}")]
        public async Task<ActionResult<CouponResponse>> GetCoupon(string code)
        {
            var g = await _promotionRepository.GetCouponByCodeAsync(code.ToUpper());
            if (g == null)
                return NotFound(new { message = "Không tìm thấy mã giảm giá" });

            return Ok(
                new CouponResponse
                {
                    MaGiamGia = g.MaGiamGia,
                    MaChienDich = g.MaChienDich,
                    TenChienDich = g.ChienDich?.TenChienDich,
                    MaGiangVien = g.MaGiangVien,
                    TenGiangVien = g.GiangVien?.HoTen,
                    LoaiGiamGia = g.LoaiGiamGia,
                    GiaTriGiam = g.GiaTriGiam,
                    GiamToiDa = g.GiamToiDa,
                    DonHangToiThieu = g.DonHangToiThieu,
                    GioiHanLuotDung = g.GioiHanLuotDung,
                    SoLuotDaDung = g.SoLuotDaDung,
                    NgayBatDau = g.NgayBatDau,
                    NgayKetThuc = g.NgayKetThuc,
                    DangHoatDong = g.DangHoatDong,
                }
            );
        }

        [Authorize(Roles = "Admin,CMO,Instructor")]
        [HttpPost("coupons")]
        public async Task<ActionResult<CouponResponse>> CreateCoupon(
            [FromBody] CreateCouponRequest request
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _promotionRepository.IsCouponCodeUniqueAsync(request.MaGiamGia))
                return Conflict(new { message = "Mã giảm giá này đã tồn tại" });

            if (request.LoaiGiamGia == 1 && (request.GiaTriGiam < 1 || request.GiaTriGiam > 100))
                return BadRequest(new { message = "Nếu giảm theo %, giá trị phải từ 1 đến 100" });

            var coupon = new GiamGia
            {
                MaGiamGia = request.MaGiamGia.ToUpper(),
                MaChienDich = request.MaChienDich,
                MaGiangVien = request.MaGiangVien,
                LoaiGiamGia = request.LoaiGiamGia,
                GiaTriGiam = request.GiaTriGiam,
                GiamToiDa = request.GiamToiDa,
                DonHangToiThieu = request.DonHangToiThieu,
                GioiHanLuotDung = request.GioiHanLuotDung,
                NgayBatDau = request.NgayBatDau,
                NgayKetThuc = request.NgayKetThuc,
                DangHoatDong = true,
            };

            await _promotionRepository.AddCouponAsync(coupon);
            return CreatedAtAction(
                nameof(GetCoupon),
                new { code = coupon.MaGiamGia },
                new { message = "Tạo mã giảm giá thành công", code = coupon.MaGiamGia }
            );
        }

        [Authorize(Roles = "Admin,CMO,Instructor")]
        [HttpPut("coupons/{code}")]
        public async Task<IActionResult> UpdateCoupon(
            string code,
            [FromBody] CreateCouponRequest request
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var coupon = await _promotionRepository.GetCouponByCodeAsync(code.ToUpper());
            if (coupon == null)
                return NotFound(new { message = "Không tìm thấy mã giảm giá" });

            // Lưu ý: Không cho sửa mã Code (PK), chỉ sửa các thông số khác
            coupon.MaChienDich = request.MaChienDich;
            coupon.LoaiGiamGia = request.LoaiGiamGia;
            coupon.GiaTriGiam = request.GiaTriGiam;
            coupon.GiamToiDa = request.GiamToiDa;
            coupon.DonHangToiThieu = request.DonHangToiThieu;
            coupon.GioiHanLuotDung = request.GioiHanLuotDung;
            coupon.NgayBatDau = request.NgayBatDau;
            coupon.NgayKetThuc = request.NgayKetThuc;

            await _promotionRepository.UpdateCouponAsync(coupon);
            return Ok(new { message = "Cập nhật mã giảm giá thành công" });
        }

        [Authorize(Roles = "Admin,CMO,Instructor")]
        [HttpPatch("coupons/{code}/status")]
        public async Task<IActionResult> ToggleCouponStatus(string code)
        {
            var success = await _promotionRepository.ToggleCouponStatusAsync(code.ToUpper());
            if (!success)
                return NotFound(new { message = "Không tìm thấy mã giảm giá" });

            return Ok(new { message = "Đã cập nhật trạng thái hoạt động của mã giảm giá" });
        }

        [Authorize(Roles = "Admin,CMO")]
        [HttpDelete("coupons/{code}")]
        public async Task<IActionResult> DeleteCoupon(string code)
        {
            var success = await _promotionRepository.DeleteCouponAsync(code.ToUpper());
            if (!success)
                return NotFound(new { message = "Không tìm thấy mã giảm giá" });

            return Ok(new { message = "Xóa cứng mã giảm giá thành công" });
        }

        [Authorize(Roles = "Admin,CMO")]
        [HttpPost("coupons/bulk-generate")]
        public async Task<IActionResult> BulkGenerate([FromBody] BulkGenerateCouponRequest request)
        {
            var coupons = new List<GiamGia>();
            var random = new Random();

            for (int i = 0; i < request.Count; i++)
            {
                string code;
                do
                {
                    code =
                        $"{request.Prefix}-{random.Next(1000, 9999)}-{Guid.NewGuid().ToString().Substring(0, 4)}".ToUpper();
                } while (
                    !await _promotionRepository.IsCouponCodeUniqueAsync(code)
                    || coupons.Any(c => c.MaGiamGia == code)
                );

                coupons.Add(
                    new GiamGia
                    {
                        MaGiamGia = code,
                        MaChienDich = request.MaChienDich,
                        LoaiGiamGia = request.LoaiGiamGia,
                        GiaTriGiam = request.GiaTriGiam,
                        GiamToiDa = request.GiamToiDa,
                        DonHangToiThieu = request.DonHangToiThieu,
                        GioiHanLuotDung = request.GioiHanLuotDung,
                        NgayBatDau = request.NgayBatDau,
                        NgayKetThuc = request.NgayKetThuc,
                    }
                );
            }

            await _promotionRepository.BulkGenerateCouponsAsync(coupons);
            return Ok(new { message = $"Đã sinh hàng loạt {request.Count} mã thành công" });
        }

        #endregion

        #region Public Validation (Student/Guest)

        [HttpGet("validate")]
        public async Task<IActionResult> ValidateCoupon(
            [FromQuery] string code,
            [FromQuery] decimal orderTotal
        )
        {
            var (isValid, message, discountAmount) = await _promotionRepository.ValidateCouponAsync(
                code.ToUpper(),
                orderTotal
            );
            if (!isValid)
                return BadRequest(new { message });

            return Ok(
                new
                {
                    message,
                    discountAmount,
                    finalTotal = orderTotal - discountAmount,
                }
            );
        }

        #endregion

        [Authorize(Roles = "Admin,CMO")]
        [HttpPost("maintenance/deactivate-expired")]
        public async Task<IActionResult> DeactivateExpired()
        {
            var count = await _promotionRepository.DeactivateExpiredPromotionsAsync();
            return Ok(new { message = $"Đã quét và vô hiệu hóa {count} mục hết hạn" });
        }
    }
}
