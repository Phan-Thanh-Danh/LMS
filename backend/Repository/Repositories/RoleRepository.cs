using System;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.DTOs.Role;
using backend.Models;
using backend.Repository.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        // ID của 7 vai trò mặc định - KHÔNG ĐƯỢC XÓA CỨNG
        private static readonly int[] SystemRoleIds = { 1, 2, 3, 4, 5, 6, 7 };

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetAllAsync()
        {
            var roles = await _context.VaiTros
                .Select(vt => new RoleResponse
                {
                    MaVaiTro = vt.MaVaiTro,
                    TenVaiTro = vt.TenVaiTro,
                    MoTa = vt.MoTa,
                    DangHoatDong = vt.DangHoatDong,
                    SoNguoiDung = _context.VaiTroNguoiDungs.Count(vr => vr.MaVaiTro == vt.MaVaiTro)
                })
                .OrderBy(vt => vt.MaVaiTro)
                .ToListAsync();

            return new OkObjectResult(new { success = true, data = roles });
        }

        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var role = await _context.VaiTros
                .Where(vt => vt.MaVaiTro == id)
                .Select(vt => new RoleResponse
                {
                    MaVaiTro = vt.MaVaiTro,
                    TenVaiTro = vt.TenVaiTro,
                    MoTa = vt.MoTa,
                    DangHoatDong = vt.DangHoatDong,
                    SoNguoiDung = _context.VaiTroNguoiDungs.Count(vr => vr.MaVaiTro == vt.MaVaiTro)
                })
                .FirstOrDefaultAsync();

            if (role == null)
                return new NotFoundObjectResult(new { success = false, message = "Vai trò không tồn tại." });

            return new OkObjectResult(new { success = true, data = role });
        }

        public async Task<IActionResult> CreateAsync(CreateRoleRequest request)
        {
            if (await _context.VaiTros.AnyAsync(vt => vt.TenVaiTro == request.TenVaiTro))
                return new ConflictObjectResult(new { success = false, message = "Tên vai trò đã tồn tại." });

            var newRole = new VaiTro
            {
                TenVaiTro = request.TenVaiTro,
                MoTa = request.MoTa,
                DangHoatDong = true,
                NgayTao = DateTime.Now
            };

            await _context.VaiTros.AddAsync(newRole);
            await _context.SaveChangesAsync();

            return new CreatedResult($"/api/v1/roles/{newRole.MaVaiTro}",
                new { success = true, message = "Tạo vai trò thành công.", data = new { newRole.MaVaiTro, newRole.TenVaiTro } });
        }

        public async Task<IActionResult> UpdateAsync(int id, UpdateRoleRequest request)
        {
            var role = await _context.VaiTros.FirstOrDefaultAsync(vt => vt.MaVaiTro == id);
            if (role == null)
                return new NotFoundObjectResult(new { success = false, message = "Vai trò không tồn tại." });

            if (request.TenVaiTro != null)
            {
                bool trungTen = await _context.VaiTros
                    .AnyAsync(vt => vt.TenVaiTro == request.TenVaiTro && vt.MaVaiTro != id);
                if (trungTen)
                    return new ConflictObjectResult(new { success = false, message = "Tên vai trò đã tồn tại." });

                role.TenVaiTro = request.TenVaiTro;
            }

            if (request.MoTa != null)
                role.MoTa = request.MoTa;

            _context.VaiTros.Update(role);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new { success = true, message = "Cập nhật vai trò thành công." });
        }

        public async Task<IActionResult> ToggleActiveStatusAsync(int id)
        {
            var role = await _context.VaiTros.FirstOrDefaultAsync(vt => vt.MaVaiTro == id);
            if (role == null)
                return new NotFoundObjectResult(new { success = false, message = "Vai trò không tồn tại." });

            role.DangHoatDong = !role.DangHoatDong;
            _context.VaiTros.Update(role);
            await _context.SaveChangesAsync();

            string trangThai = role.DangHoatDong ? "kích hoạt" : "vô hiệu hóa";
            return new OkObjectResult(new { success = true, message = $"Vai trò đã được {trangThai}." });
        }

        public async Task<IActionResult> HardDeleteAsync(int id)
        {
            // Không cho xóa 7 vai trò hệ thống mặc định
            if (Array.Exists(SystemRoleIds, sysId => sysId == id))
                return new BadRequestObjectResult(new { success = false, message = "Không thể xóa các vai trò mặc định của hệ thống (ID 1-7)." });

            var role = await _context.VaiTros.FirstOrDefaultAsync(vt => vt.MaVaiTro == id);
            if (role == null)
                return new NotFoundObjectResult(new { success = false, message = "Vai trò không tồn tại." });

            // Không cho xóa nếu còn user đang được gán vai trò này
            bool coNguoiDung = await _context.VaiTroNguoiDungs.AnyAsync(vr => vr.MaVaiTro == id);
            if (coNguoiDung)
                return new BadRequestObjectResult(new { success = false, message = "Không thể xóa: vai trò này đang được gán cho người dùng. Hãy vô hiệu hóa thay vì xóa." });

            _context.VaiTros.Remove(role);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new { success = true, message = "Đã xóa vĩnh viễn vai trò." });
        }
    }
}
