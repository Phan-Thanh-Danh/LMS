using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.DTOs.Instructor;
using backend.Models;
using backend.Repository.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Repositories
{
    public class ModeratorRepository : IModeratorRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IStorageService _storageService;
        private readonly IEmailService _emailService;

        public ModeratorRepository(
            ApplicationDbContext context,
            IStorageService storageService,
            IEmailService emailService
        )
        {
            _context = context;
            _storageService = storageService;
            _emailService = emailService;
        }

        public async Task<IActionResult> GetApprovedInstructorsAsync()
        {
            var result = await _context.HoSoGiangViens
                .Include(h => h.NguoiDung)
                .Where(h => h.TrangThaiKYC == "Approved")
                .Select(h => new 
                {
                    h.MaGiangVien,
                    h.NguoiDung.HoTen,
                    h.NguoiDung.Email,
                    h.NguoiDung.NgayTao,
                    h.NgayCapNhat,
                    TotalCourses = _context.KhoaHocs.Count(k => k.MaGiangVien == h.MaGiangVien)
                })
                .OrderByDescending(h => h.NgayCapNhat)
                .ToListAsync();

            return new OkObjectResult(result);
        }

        public async Task<IActionResult> GetAllKycAsync(string? status)
        {
            var query = _context.HoSoGiangViens.Include(h => h.NguoiDung).AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(h => h.TrangThaiKYC == status);
            }

            var result = await query
                .Select(h => new
                {
                    h.MaGiangVien,
                    h.NguoiDung.HoTen,
                    h.NguoiDung.Email,
                    h.TrangThaiKYC,
                    h.NgayCapNhat,
                })
                .OrderByDescending(h => h.NgayCapNhat)
                .ToListAsync();

            return new OkObjectResult(result);
        }

        public async Task<IActionResult> GetKycDetailsAsync(Guid instructorId)
        {
            var profile = await _context
                .HoSoGiangViens.Include(h => h.NguoiDung)
                .FirstOrDefaultAsync(h => h.MaGiangVien == instructorId);

            if (profile == null)
                return new NotFoundObjectResult(
                    new { message = "Không tìm thấy hồ sơ giảng viên." }
                );

            // Lấy URL thực tế từ bảng TaiNguyenSo
            var cccdFront = await _context
                .TaiNguyenSos.Where(a => a.MaTaiNguyen == profile.MaTaiNguyenCCCDMatTruoc)
                .Select(a => a.DuongDanLuuTru)
                .FirstOrDefaultAsync();

            var cccdBack = await _context
                .TaiNguyenSos.Where(a => a.MaTaiNguyen == profile.MaTaiNguyenCCCDMatSau)
                .Select(a => a.DuongDanLuuTru)
                .FirstOrDefaultAsync();

            var certUrls = new List<string>();
            if (!string.IsNullOrEmpty(profile.DanhSachMaTaiNguyenBangCap))
            {
                var certIds = profile
                    .DanhSachMaTaiNguyenBangCap.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => Guid.TryParse(s, out var g) ? g : Guid.Empty)
                    .Where(g => g != Guid.Empty)
                    .ToList();

                certUrls = await _context
                    .TaiNguyenSos.Where(a => certIds.Contains(a.MaTaiNguyen))
                    .Select(a => a.DuongDanLuuTru!)
                    .ToListAsync();
            }

            return new OkObjectResult(
                new
                {
                    profile.MaGiangVien,
                    profile.NguoiDung.HoTen,
                    profile.NguoiDung.Email,
                    profile.TrangThaiKYC,
                    profile.LyDoTuChoi,
                    profile.ThongTinNganHangMaHoa,
                    profile.MaSoThueMaHoa,
                    CCCDFrontUrl = cccdFront,
                    CCCDBackUrl = cccdBack,
                    CertificateUrls = certUrls,
                    profile.NgayCapNhat,
                }
            );
        }

        public async Task<IActionResult> ReviewKycAsync(Guid instructorId, KycReviewDto request)
        {
            var profile = await _context
                .HoSoGiangViens.Include(h => h.NguoiDung)
                .FirstOrDefaultAsync(h => h.MaGiangVien == instructorId);

            if (profile == null)
                return new NotFoundObjectResult(
                    new { message = "Không tìm thấy hồ sơ giảng viên." }
                );

            if (request.IsApproved)
            {
                profile.TrangThaiKYC = "Approved";
                profile.LyDoTuChoi = null;
                profile.NgayCapNhat = DateTime.Now;

                await _context.SaveChangesAsync();

                // Gửi email chúc mừng (HTML Template)
                var subject = "[AET Academy] Hồ sơ giảng viên của bạn đã được phê duyệt";
                var body =
                    $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 10px;'>
                    <h2 style='color: #28a745; text-align: center;'>Chúc mừng {profile.NguoiDung.HoTen}!</h2>
                    <p>Hồ sơ giảng viên (KYC) của bạn đã được đội ngũ quản trị AET Academy phê duyệt thành công.</p>
                    <div style='background-color: #f8f9fa; padding: 15px; border-radius: 5px; margin: 20px 0;'>
                        <p><strong>Trạng thái:</strong> Đã kích hoạt</p>
                        <p><strong>Quyền hạn:</strong> Instructor Studio (Tạo khoa học, Soạn bài giảng)</p>
                    </div>
                    <p>Bây giờ bạn đã có thể bắt đầu hành trình chia sẻ kiến thức của mình. Hãy đăng nhập và khám phá không gian làm việc mới nhé!</p>
                    <hr style='border: 0; border-top: 1px solid #eeeeee; margin: 20px 0;'>
                    <p style='font-size: 12px; color: #777; text-align: center;'>Chào mừng bạn gia nhập đội ngũ chuyên gia của AET Academy.</p>
                </div>";

                await _emailService.SendEmailAsync(profile.NguoiDung.Email, subject, body);

                return new OkObjectResult(
                    new { message = "Đã phê duyệt hồ sơ giảng viên thành công." }
                );
            }
            else
            {
                // XỬ LÝ TỪ CHỐI & DỌN DẸP
                var assetIdsToDelete = new List<Guid>();
                if (profile.MaTaiNguyenCCCDMatTruoc.HasValue)
                    assetIdsToDelete.Add(profile.MaTaiNguyenCCCDMatTruoc.Value);
                if (profile.MaTaiNguyenCCCDMatSau.HasValue)
                    assetIdsToDelete.Add(profile.MaTaiNguyenCCCDMatSau.Value);

                if (!string.IsNullOrEmpty(profile.DanhSachMaTaiNguyenBangCap))
                {
                    var certIds = profile
                        .DanhSachMaTaiNguyenBangCap.Split(
                            ',',
                            StringSplitOptions.RemoveEmptyEntries
                        )
                        .Select(s => Guid.TryParse(s, out var g) ? g : Guid.Empty)
                        .Where(g => g != Guid.Empty);
                    assetIdsToDelete.AddRange(certIds);
                }

                foreach (var assetId in assetIdsToDelete)
                {
                    var asset = await _context.TaiNguyenSos.FirstOrDefaultAsync(a =>
                        a.MaTaiNguyen == assetId
                    );
                    if (asset != null)
                    {
                        if (!string.IsNullOrEmpty(asset.DuongDanLuuTru))
                        {
                            await _storageService.DeleteFileAsync(asset.DuongDanLuuTru);
                        }
                        _context.TaiNguyenSos.Remove(asset);
                    }
                }

                profile.TrangThaiKYC = "Rejected";
                profile.LyDoTuChoi = request.RejectionReason;
                profile.MaTaiNguyenCCCDMatTruoc = null;
                profile.MaTaiNguyenCCCDMatSau = null;
                profile.DanhSachMaTaiNguyenBangCap = null;
                profile.NgayCapNhat = DateTime.Now;

                await _context.SaveChangesAsync();

                // Gửi email thông báo từ chối (HTML Template)
                var subject = "[AET Academy] Thông báo hồ sơ giảng viên chưa đạt yêu cầu";
                var body =
                    $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 10px;'>
                    <h2 style='color: #dc3545; text-align: center;'>Thông báo xác thực hồ sơ</h2>
                    <p>Chào {profile.NguoiDung.HoTen}, đội ngũ kiểm duyệt đã xem xét hồ sơ của bạn.</p>
                    <p>Rất tiếc, hồ sơ hiện tại chưa đạt yêu cầu để kích hoạt quyền Giảng viên vì lý do sau:</p>
                    <div style='background-color: #fff3f3; border-left: 5px solid #dc3545; padding: 15px; margin: 20px 0;'>
                        <strong>Lý do:</strong> {request.RejectionReason}
                    </div>
                    <p style='color: #666;'>Vì lý do bảo mật thông tin, toàn bộ tài liệu bạn đã nộp (CCCD, bằng cấp) đã được hệ thống xóa bỏ. Vui lòng nộp lại hồ sơ mới chính xác hơn.</p>
                    <hr style='border: 0; border-top: 1px solid #eeeeee; margin: 20px 0;'>
                    <p style='font-size: 12px; color: #777; text-align: center;'>AET Academy - Chất lượng là ưu tiên hàng đầu.</p>
                </div>";

                await _emailService.SendEmailAsync(profile.NguoiDung.Email, subject, body);

                return new OkObjectResult(
                    new { message = "Đã từ chối hồ sơ và dọn dẹp tài nguyên thành công." }
                );
            }
        }
    }
}
