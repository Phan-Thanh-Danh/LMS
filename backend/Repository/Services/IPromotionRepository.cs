using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Repository.Services
{
    public interface IPromotionRepository
    {
        // Campaigns
        Task<IEnumerable<ChienDich>> GetCampaignsAsync();
        Task<ChienDich?> GetCampaignByIdAsync(int id);
        Task<bool> AddCampaignAsync(ChienDich campaign);
        Task<bool> UpdateCampaignAsync(ChienDich campaign);
        Task<bool> DeleteCampaignAsync(int id);
        Task<bool> ToggleCampaignStatusAsync(int id);

        // Coupons
        Task<IEnumerable<GiamGia>> GetCouponsAsync(
            int? campaignId = null,
            Guid? instructorId = null
        );
        Task<GiamGia?> GetCouponByCodeAsync(string code);
        Task<bool> AddCouponAsync(GiamGia coupon);
        Task<bool> UpdateCouponAsync(GiamGia coupon);
        Task<bool> DeleteCouponAsync(string code);
        Task<bool> ToggleCouponStatusAsync(string code);
        Task<bool> IsCouponCodeUniqueAsync(string code);

        // Validation
        Task<(bool isValid, string message, decimal discountAmount)> ValidateCouponAsync(
            string code,
            decimal orderTotal,
            List<int>? courseIds = null
        );

        // Automation
        Task<int> BulkGenerateCouponsAsync(IEnumerable<GiamGia> coupons);
        Task<int> DeactivateExpiredPromotionsAsync();
    }
}
