using System;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Repository.Services
{
    public interface IMediaRepository
    {
        Task<TaiNguyenSo?> GetAssetByIdAsync(Guid assetId);
        Task<TaiNguyenSo> CreateAssetAsync(TaiNguyenSo asset);
        Task UpdateAssetAsync(TaiNguyenSo asset);
        Task DeleteAssetAsync(Guid assetId);
        Task<System.Collections.Generic.List<TaiNguyenSo>> GetAssetsByUserIdAsync(Guid userId);
    }
}
