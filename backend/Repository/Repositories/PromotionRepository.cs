using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using backend.Repository.Services;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Repositories
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly ApplicationDbContext _context;

        public PromotionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Campaigns

        public async Task<IEnumerable<ChienDich>> GetCampaignsAsync()
        {
            return await _context.ChienDichs.OrderByDescending(c => c.NgayTao).ToListAsync();
        }

        public async Task<ChienDich?> GetCampaignByIdAsync(int id)
        {
            return await _context.ChienDichs.FindAsync(id);
        }

        public async Task<bool> AddCampaignAsync(ChienDich campaign)
        {
            _context.ChienDichs.Add(campaign);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCampaignAsync(ChienDich campaign)
        {
            _context.ChienDichs.Update(campaign);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCampaignAsync(int id)
        {
            var campaign = await _context.ChienDichs.FindAsync(id);
            if (campaign == null) return false;

            _context.ChienDichs.Remove(campaign);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ToggleCampaignStatusAsync(int id)
        {
            var campaign = await _context.ChienDichs.FindAsync(id);
            if (campaign == null) return false;

            campaign.DangHoatDong = !campaign.DangHoatDong;
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Coupons

        public async Task<IEnumerable<GiamGia>> GetCouponsAsync(int? campaignId = null, Guid? instructorId = null)
        {
            var query = _context.GiamGias.Include(g => g.ChienDich).Include(g => g.GiangVien).AsQueryable();

            if (campaignId.HasValue)
                query = query.Where(g => g.MaChienDich == campaignId.Value);
            
            if (instructorId.HasValue)
                query = query.Where(g => g.MaGiangVien == instructorId.Value);

            return await query.OrderByDescending(g => g.NgayTao).ToListAsync();
        }

        public async Task<GiamGia?> GetCouponByCodeAsync(string code)
        {
            return await _context.GiamGias
                .Include(g => g.ChienDich)
                .Include(g => g.GiangVien)
                .FirstOrDefaultAsync(g => g.MaGiamGia == code);
        }

        public async Task<bool> AddCouponAsync(GiamGia coupon)
        {
            _context.GiamGias.Add(coupon);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCouponAsync(GiamGia coupon)
        {
            _context.GiamGias.Update(coupon);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCouponAsync(string code)
        {
            var coupon = await _context.GiamGias.FindAsync(code);
            if (coupon == null) return false;

            _context.GiamGias.Remove(coupon);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ToggleCouponStatusAsync(string code)
        {
            var coupon = await _context.GiamGias.FindAsync(code);
            if (coupon == null) return false;

            coupon.DangHoatDong = !coupon.DangHoatDong;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> IsCouponCodeUniqueAsync(string code)
        {
            return !await _context.GiamGias.AnyAsync(g => g.MaGiamGia == code);
        }

        #endregion

        #region Business Logic & Automation

        public async Task<(bool isValid, string message, decimal discountAmount)> ValidateCouponAsync(string code, decimal orderTotal, List<int>? courseIds = null)
        {
            var coupon = await _context.GiamGias.FindAsync(code);
            if (coupon == null || !coupon.DangHoatDong)
                return (false, "Mã không tồn tại hoặc đã bị vô hiệu hóa", 0);

            var now = DateTime.Now;
            if (now < coupon.NgayBatDau)
                return (false, $"Mã chưa có hiệu lực. Có hiệu lực từ {coupon.NgayBatDau:dd/MM/yyyy}", 0);
            
            if (now > coupon.NgayKetThuc)
                return (false, "Mã đã hết hạn sử dụng", 0);

            if (coupon.GioiHanLuotDung.HasValue && coupon.SoLuotDaDung >= coupon.GioiHanLuotDung.Value)
                return (false, "Mã đã hết lượt sử dụng (Limit reached)", 0);

            if (orderTotal < coupon.DonHangToiThieu)
                return (false, $"Chưa đạt giá trị đơn hàng tối thiểu ({coupon.DonHangToiThieu:N0}đ)", 0);

            decimal discount = 0;
            if (coupon.LoaiGiamGia == 1) // Percent
            {
                discount = orderTotal * (coupon.GiaTriGiam / 100);
                if (coupon.GiamToiDa.HasValue && discount > coupon.GiamToiDa.Value)
                    discount = coupon.GiamToiDa.Value;
            }
            else // Fixed Amount
            {
                discount = coupon.GiaTriGiam;
            }

            // Đảm bảo mức giảm không vượt quá tổng tiền
            if (discount > orderTotal) discount = orderTotal;

            return (true, "Áp mã thành công", discount);
        }

        public async Task<int> BulkGenerateCouponsAsync(IEnumerable<GiamGia> coupons)
        {
            _context.GiamGias.AddRange(coupons);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeactivateExpiredPromotionsAsync()
        {
            var now = DateTime.Now;

            // Hết hạn chiến dịch
            var expiredCampaigns = await _context.ChienDichs
                .Where(c => c.DangHoatDong && c.NgayKetThuc < now)
                .ToListAsync();
            
            foreach (var c in expiredCampaigns) c.DangHoatDong = false;

            // Hết hạn mã giảm giá
            var expiredCoupons = await _context.GiamGias
                .Where(g => g.DangHoatDong && g.NgayKetThuc < now)
                .ToListAsync();
                
            foreach (var g in expiredCoupons) g.DangHoatDong = false;

            return await _context.SaveChangesAsync();
        }

        #endregion
    }
}
