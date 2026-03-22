using backend.Data;
using backend.DTOs.Auth;
using backend.Helpers;
using backend.Models;
using backend.Repository.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Repositories
{
    public class AuthRepository : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthRepository(
            ApplicationDbContext context,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            if (await _context.NguoiDungs.AnyAsync(u => u.Email == request.Email && !u.DaXoa))
            {
                return new ConflictObjectResult(new { message = "Email này đã được sử dụng." });
            }

            var salt = PasswordHelper.GenerateSalt();
            var hashedPassword = PasswordHelper.HashPassword(request.Password, salt);

            var user = new NguoiDung
            {
                Email = request.Email,
                MatKhauBam = hashedPassword,
                MuoiMatKhau = salt,
                HoTen = request.HoTen,
                NgayTao = DateTime.Now,
                DangHoatDong = true,
                DaXoa = false,
            };

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.NguoiDungs.Add(user);
                await _context.SaveChangesAsync();

                var roleName = request.Role.Equals("Instructor", StringComparison.OrdinalIgnoreCase)
                    ? "Instructor"
                    : "Student";
                var role = await _context.VaiTros.FirstOrDefaultAsync(r => r.TenVaiTro == roleName);

                if (role != null)
                {
                    _context.VaiTroNguoiDungs.Add(
                        new VaiTroNguoiDung
                        {
                            MaNguoiDung = user.MaNguoiDung,
                            MaVaiTro = role.MaVaiTro,
                            NgayGanVaiTro = DateTime.Now,
                        }
                    );

                    if (roleName == "Instructor")
                    {
                        var instructorProfile = new HoSoGiangVien
                        {
                            MaGiangVien = user.MaNguoiDung,
                            TrangThaiKYC = "Pending",
                            TyLeDoanhThu = 70,
                            NgayCapNhat = DateTime.Now,
                        };
                        _context.HoSoGiangViens.Add(instructorProfile);
                    }

                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();

                return new CreatedResult(
                    "",
                    new
                    {
                        message = "Đăng ký tài khoản thành công.",
                        userId = user.MaNguoiDung,
                        email = user.Email,
                    }
                );
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new ObjectResult(
                    new
                    {
                        message = "Đã có lỗi xảy ra trong quá trình đăng ký.",
                        error = ex.Message,
                    }
                )
                {
                    StatusCode = 500,
                };
            }
        }

        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            // Sử dụng Email từ request
            var user = await _context.NguoiDungs.FirstOrDefaultAsync(u =>
                u.Email == request.Email && !u.DaXoa
            );

            if (user == null)
            {
                return new UnauthorizedObjectResult(
                    new { message = "Email hoặc mật khẩu không chính xác." }
                );
            }

            if (!user.DangHoatDong)
            {
                return new ObjectResult(new { message = "Tài khoản của bạn đã bị khóa." })
                {
                    StatusCode = 403,
                };
            }

            if (!PasswordHelper.VerifyPassword(request.Password, user.MuoiMatKhau, user.MatKhauBam))
            {
                return new UnauthorizedObjectResult(
                    new { message = "Email hoặc mật khẩu không chính xác." }
                );
            }

            var roles = await _context
                .VaiTroNguoiDungs.Where(vr => vr.MaNguoiDung == user.MaNguoiDung)
                .Include(vr => vr.VaiTro)
                .Select(vr => new RoleDto { Id = vr.MaVaiTro, Name = vr.VaiTro.TenVaiTro })
                .ToListAsync();

            var roleNames = roles.Select(r => r.Name).ToList();

            var accessToken = JwtHelper.GenerateAccessToken(user, roleNames, _configuration);
            var refreshToken = JwtHelper.GenerateRefreshToken();

            var httpContext = _httpContextAccessor.HttpContext;
            var session = new PhienLamViec
            {
                MaNguoiDung = user.MaNguoiDung,
                TokenLamMoi = refreshToken,
                DiaChiIP = httpContext?.Connection.RemoteIpAddress?.ToString(),
                ThongTinThietBi = httpContext?.Request.Headers["User-Agent"].ToString(),
                HetHanLuc = DateTime.Now.AddDays(7),
                DaThuHoi = false,
                NgayTao = DateTime.Now,
            };

            _context.PhienLamViecs.Add(session);

            user.LanDangNhapCuoi = DateTime.Now;
            _context.NguoiDungs.Update(user);

            await _context.SaveChangesAsync();

            return new OkObjectResult(
                new LoginResponse
                {
                    Token = accessToken,
                    RefreshToken = refreshToken,
                    User = new UserDto
                    {
                        Id = user.MaNguoiDung,
                        Email = user.Email,
                        HoTen = user.HoTen,
                        Roles = roles,
                    },
                }
            );
        }
    }
}
