using System;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using backend.Repository.Services;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Repositories
{
    public class MediaRepository : IMediaRepository
    {
        private readonly ApplicationDbContext _context;

        public MediaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaiNguyenSo?> GetAssetByIdAsync(Guid assetId)
        {
            return await _context.TaiNguyenSos.FirstOrDefaultAsync(t => t.MaTaiNguyen == assetId);
        }

        public async Task<TaiNguyenSo> CreateAssetAsync(TaiNguyenSo asset)
        {
            _context.TaiNguyenSos.Add(asset);
            await _context.SaveChangesAsync();
            return asset;
        }

        public async Task UpdateAssetAsync(TaiNguyenSo asset)
        {
            _context.TaiNguyenSos.Update(asset);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAssetAsync(Guid assetId)
        {
            var asset = await _context.TaiNguyenSos.FindAsync(assetId);
            if (asset != null)
            {
                _context.TaiNguyenSos.Remove(asset);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<System.Collections.Generic.List<TaiNguyenSo>> GetAssetsByUserIdAsync(
            Guid userId
        )
        {
            return await _context
                .TaiNguyenSos.Where(a => a.TaiLenBoi == userId)
                .OrderByDescending(a => a.NgayTao)
                .ToListAsync();
        }
    }
}
