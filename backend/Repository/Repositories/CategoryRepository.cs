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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DanhMuc>> GetGroupsAsync()
        {
            return await _context
                .DanhMucs.Where(d => d.MaDanhMucCha == null && !d.DaXoa)
                .OrderBy(d => d.ThuTuHienThi)
                .ToListAsync();
        }

        public async Task<DanhMuc?> GetGroupByIdAsync(int id)
        {
            return await _context.DanhMucs.FirstOrDefaultAsync(d =>
                d.MaDanhMuc == id && d.MaDanhMucCha == null && !d.DaXoa
            );
        }

        public async Task<IEnumerable<DanhMuc>> GetSubCategoriesAsync(int? parentId = null)
        {
            var query = _context
                .DanhMucs.Include(d => d.DanhMucCha)
                .Where(d => d.MaDanhMucCha != null && !d.DaXoa);

            if (parentId.HasValue)
            {
                query = query.Where(d => d.MaDanhMucCha == parentId.Value);
            }

            return await query.OrderBy(d => d.ThuTuHienThi).ToListAsync();
        }

        public async Task<DanhMuc?> GetSubCategoryByIdAsync(int id)
        {
            return await _context
                .DanhMucs.Include(d => d.DanhMucCha)
                .FirstOrDefaultAsync(d => d.MaDanhMuc == id && d.MaDanhMucCha != null && !d.DaXoa);
        }

        public async Task<bool> AddCategoryAsync(DanhMuc category)
        {
            category.NgayTao = DateTime.Now;
            _context.DanhMucs.Add(category);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateGroupAsync(DanhMuc group)
        {
            group.NgayCapNhat = DateTime.Now;
            _context.DanhMucs.Update(group);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> HardDeleteGroupAsync(int id)
        {
            var group = await _context.DanhMucs.FindAsync(id);
            if (group == null)
                return false;

            _context.DanhMucs.Remove(group);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ToggleStatusAsync(int id)
        {
            var group = await _context.DanhMucs.FindAsync(id);
            if (group == null || group.DaXoa)
                return false;

            group.DangHienThi = !group.DangHienThi;
            group.NgayCapNhat = DateTime.Now;
            _context.DanhMucs.Update(group);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> IsSlugUniqueAsync(string slug, int? excludeId = null)
        {
            return !await _context.DanhMucs.AnyAsync(d =>
                d.DuongDanURL == slug && (!excludeId.HasValue || d.MaDanhMuc != excludeId.Value)
            );
        }

        public async Task<bool> HasChildrenAsync(int groupId)
        {
            return await _context.DanhMucs.AnyAsync(d => d.MaDanhMucCha == groupId && !d.DaXoa);
        }

        public async Task<bool> HasCoursesAsync(int groupId)
        {
            return await _context.KhoaHocs.AnyAsync(k => k.MaDanhMuc == groupId && !k.DaXoa);
        }
    }
}
