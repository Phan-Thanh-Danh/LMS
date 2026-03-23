using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.DTOs.User;
using backend.Helpers;
using backend.Models;
using backend.Repository.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetAllWithRolesAsync(UserQueryParameters query)
        {
            var usersQuery = _context.NguoiDungs.Where(u => !u.DaXoa).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                var search = query.SearchTerm.Trim().ToLower();
                usersQuery = usersQuery.Where(u =>
                    u.HoTen.ToLower().Contains(search) || u.Email.ToLower().Contains(search)
                );
            }

            if (query.RoleId.HasValue)
            {
                usersQuery = usersQuery.Where(u =>
                    _context.VaiTroNguoiDungs.Any(vr =>
                        vr.MaNguoiDung == u.MaNguoiDung && vr.MaVaiTro == query.RoleId.Value
                    )
                );
            }

            var totalCount = await usersQuery.CountAsync();

            var users = await usersQuery
                .OrderByDescending(u => u.NgayTao)
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(u => new UserResponse
                {
                    MaNguoiDung = u.MaNguoiDung,
                    Email = u.Email,
                    HoTen = u.HoTen,
                    DuongDanAnhDaiDien = u.DuongDanAnhDaiDien,
                    TieuSu = u.TieuSu,
                    DangHoatDong = u.DangHoatDong,
                    EmailDaXacThuc = u.EmailDaXacThuc,
                    LaNhanVien = u.LaNhanVien,
                    NgayTao = u.NgayTao,
                    Roles = _context
                        .VaiTroNguoiDungs.Where(vr => vr.MaNguoiDung == u.MaNguoiDung)
                        .Select(vr => vr.MaVaiTro)
                        .ToList(),
                })
                .ToListAsync();

            return new OkObjectResult(
                new
                {
                    success = true,
                    data = users,
                    meta = new
                    {
                        page = query.Page,
                        pageSize = query.PageSize,
                        total = totalCount,
                    },
                }
            );
        }

        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var user = await _context
                .NguoiDungs.Where(u => u.MaNguoiDung == id && !u.DaXoa)
                .Select(u => new UserResponse
                {
                    MaNguoiDung = u.MaNguoiDung,
                    Email = u.Email,
                    HoTen = u.HoTen,
                    DuongDanAnhDaiDien = u.DuongDanAnhDaiDien,
                    TieuSu = u.TieuSu,
                    DangHoatDong = u.DangHoatDong,
                    EmailDaXacThuc = u.EmailDaXacThuc,
                    LaNhanVien = u.LaNhanVien,
                    NgayTao = u.NgayTao,
                    Roles = _context
                        .VaiTroNguoiDungs.Where(vr => vr.MaNguoiDung == u.MaNguoiDung)
                        .Select(vr => vr.MaVaiTro)
                        .ToList(),
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return new NotFoundObjectResult(
                    new { success = false, message = "Người dùng không tồn tại." }
                );
            }

            return new OkObjectResult(new { success = true, data = user });
        }

        public async Task<IActionResult> CreateUserAsync(CreateUserRequest request)
        {
            if (await _context.NguoiDungs.AnyAsync(u => u.Email == request.Email))
            {
                return new ConflictObjectResult(
                    new { success = false, message = "Email đã tồn tại." }
                );
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var salt = PasswordHelper.GenerateSalt();
                var hashedPassword = PasswordHelper.HashPassword(request.Password, salt);

                var newUser = new NguoiDung
                {
                    MaNguoiDung = Guid.NewGuid(),
                    Email = request.Email,
                    MatKhauBam = hashedPassword,
                    MuoiMatKhau = salt,
                    HoTen = request.HoTen,
                    DuongDanAnhDaiDien = request.DuongDanAnhDaiDien,
                    TieuSu = request.TieuSu,
                    EmailDaXacThuc = true, // Admin tạo thì mặc định xác thực
                    NgayTao = DateTime.Now,
                };

                await _context.NguoiDungs.AddAsync(newUser);
                await _context.SaveChangesAsync();

                var roleIds =
                    request.RoleIds != null && request.RoleIds.Any()
                        ? request.RoleIds
                        : new List<int> { 1 }; // Mặc định role 1 (Student)

                foreach (var roleId in roleIds.Distinct())
                {
                    if (await _context.VaiTros.AnyAsync(v => v.MaVaiTro == roleId))
                    {
                        await _context.VaiTroNguoiDungs.AddAsync(
                            new VaiTroNguoiDung
                            {
                                MaNguoiDung = newUser.MaNguoiDung,
                                MaVaiTro = roleId,
                            }
                        );

                        if (roleId == 2) // Giảng viên
                        {
                            await _context.HoSoGiangViens.AddAsync(
                                new HoSoGiangVien
                                {
                                    MaGiangVien = newUser.MaNguoiDung,
                                    TrangThaiKYC = "Pending", // Chờ duyệt KYC
                                }
                            );
                        }

                        if (roleId >= 3) // Nhân viên
                        {
                            newUser.LaNhanVien = true;
                        }
                    }
                }

                _context.NguoiDungs.Update(newUser); // Cập nhật lại cờ LaNhanVien nếu có
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new CreatedResult(
                    $"/api/v1/users/{newUser.MaNguoiDung}",
                    new { success = true, message = "Tạo người dùng thành công." }
                );
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new ObjectResult(
                    new
                    {
                        success = false,
                        message = "Lỗi khi tạo tài khoản.",
                        error = ex.Message,
                    }
                )
                {
                    StatusCode = 500,
                };
            }
        }

        public async Task<IActionResult> UpdateUserAsync(Guid id, UpdateUserRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = await _context.NguoiDungs.FirstOrDefaultAsync(u =>
                    u.MaNguoiDung == id && !u.DaXoa
                );
                if (user == null)
                {
                    return new NotFoundObjectResult(
                        new { success = false, message = "Người dùng không tồn tại." }
                    );
                }

                if (request.HoTen != null)
                    user.HoTen = request.HoTen;
                if (request.DuongDanAnhDaiDien != null)
                    user.DuongDanAnhDaiDien = request.DuongDanAnhDaiDien;
                if (request.TieuSu != null)
                    user.TieuSu = request.TieuSu;

                user.NgayCapNhat = DateTime.Now;

                // Xử lý cập nhật Role
                if (request.RoleIds != null)
                {
                    var currentRoles = await _context
                        .VaiTroNguoiDungs.Where(vr => vr.MaNguoiDung == id)
                        .ToListAsync();
                    _context.VaiTroNguoiDungs.RemoveRange(currentRoles);

                    bool isNhanVien = false;
                    foreach (var roleId in request.RoleIds.Distinct())
                    {
                        if (await _context.VaiTros.AnyAsync(v => v.MaVaiTro == roleId))
                        {
                            await _context.VaiTroNguoiDungs.AddAsync(
                                new VaiTroNguoiDung { MaNguoiDung = id, MaVaiTro = roleId }
                            );

                            if (roleId >= 3)
                                isNhanVien = true;
                        }
                    }
                    user.LaNhanVien = isNhanVien;
                }

                _context.NguoiDungs.Update(user);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new OkObjectResult(
                    new { success = true, message = "Cập nhật thông tin thành công." }
                );
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new ObjectResult(
                    new
                    {
                        success = false,
                        message = "Lỗi hệ thống.",
                        error = ex.Message,
                    }
                )
                {
                    StatusCode = 500,
                };
            }
        }

        public async Task<IActionResult> ToggleActiveStatusAsync(Guid id)
        {
            var user = await _context.NguoiDungs.FirstOrDefaultAsync(u =>
                u.MaNguoiDung == id && !u.DaXoa
            );
            if (user == null)
            {
                return new NotFoundObjectResult(
                    new { success = false, message = "Người dùng không tồn tại." }
                );
            }

            user.DangHoatDong = !user.DangHoatDong;
            user.NgayCapNhat = DateTime.Now;

            _context.NguoiDungs.Update(user);

            // Tùy chọn: Nếu bị khóa (DangHoatDong = false), thu hồi phiên làm việc
            if (!user.DangHoatDong)
            {
                var sessions = await _context
                    .PhienLamViecs.Where(s => s.MaNguoiDung == id && !s.DaThuHoi)
                    .ToListAsync();
                foreach (var session in sessions)
                {
                    session.DaThuHoi = true;
                }
                _context.PhienLamViecs.UpdateRange(sessions);
            }

            await _context.SaveChangesAsync();

            string status = user.DangHoatDong ? "mở khóa" : "bị khóa";
            return new OkObjectResult(
                new { success = true, message = $"Tài khoản đã {status} thành công." }
            );
        }

        public async Task<IActionResult> SoftDeleteAsync(Guid id)
        {
            var user = await _context.NguoiDungs.FirstOrDefaultAsync(u =>
                u.MaNguoiDung == id && !u.DaXoa
            );
            if (user == null)
            {
                return new NotFoundObjectResult(
                    new { success = false, message = "Người dùng không tồn tại." }
                );
            }

            user.DaXoa = true;
            user.NgayCapNhat = DateTime.Now;

            _context.NguoiDungs.Update(user);
            await _context.SaveChangesAsync();

            return new OkObjectResult(
                new { success = true, message = "Đã xóa người dùng vào thùng rác." }
            );
        }

        public async Task<IActionResult> HardDeleteAsync(Guid id)
        {
            var user = await _context.NguoiDungs.FirstOrDefaultAsync(u =>
                u.MaNguoiDung == id
            );

            if (user == null)
            {
                return new NotFoundObjectResult(
                    new { success = false, message = "Người dùng không tồn tại." }
                );
            }

            // Kiểm tra điều kiện: email chưa xác thực
            if (user.EmailDaXacThuc)
            {
                return new BadRequestObjectResult(
                    new { success = false, message = "Chỉ được xóa cứng tài khoản chưa xác thực email." }
                );
            }

            // Kiểm tra điều kiện: chưa có giao dịch hay ghi danh nào
            bool coGiaoDich = await _context.DonHangs.AnyAsync(o => o.MaHocVien == id);
            bool coGhiDanh = await _context.GhiDanhs.AnyAsync(e => e.MaHocVien == id);

            if (coGiaoDich || coGhiDanh)
            {
                return new BadRequestObjectResult(
                    new { success = false, message = "Không thể xóa cứng: tài khoản đã có lịch sử giao dịch hoặc ghi danh. Hãy dùng Xóa mềm." }
                );
            }

            // Xóa vai trò liên kết trước
            var roles = await _context.VaiTroNguoiDungs
                .Where(vr => vr.MaNguoiDung == id).ToListAsync();
            _context.VaiTroNguoiDungs.RemoveRange(roles);

            // Xóa phiên làm việc
            var sessions = await _context.PhienLamViecs
                .Where(s => s.MaNguoiDung == id).ToListAsync();
            _context.PhienLamViecs.RemoveRange(sessions);

            // Xóa OTP
            var otps = await _context.MaOtps
                .Where(o => o.MaNguoiDung == id).ToListAsync();
            _context.MaOtps.RemoveRange(otps);

            // Xóa vĩnh viễn tài khoản
            _context.NguoiDungs.Remove(user);
            await _context.SaveChangesAsync();

            return new OkObjectResult(
                new { success = true, message = "Đã xóa vĩnh viễn tài khoản." }
            );
        }
    }
}
