using System;
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
    public class InstructorRepository : IInstructorRepository
    {
        private readonly ApplicationDbContext _context;

        public InstructorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HoSoGiangVien?> GetProfileAsync(Guid instructorId)
        {
            return await _context
                .HoSoGiangViens.Include(h => h.NguoiDung)
                .FirstOrDefaultAsync(h => h.MaGiangVien == instructorId);
        }

        public async Task<IActionResult> SubmitKycAsync(Guid instructorId, KycSubmissionDto request)
        {
            var profile = await _context.HoSoGiangViens.FirstOrDefaultAsync(h =>
                h.MaGiangVien == instructorId
            );
            if (profile == null)
                return new NotFoundObjectResult(
                    new { message = "Không tìm thấy hồ sơ giảng viên." }
                );

            // 1. Cập nhật thông tin KYC
            profile.MaTaiNguyenCCCDMatTruoc = request.CCCDFrontAssetId;
            profile.MaTaiNguyenCCCDMatSau = request.CCCDBackAssetId;
            profile.DanhSachMaTaiNguyenBangCap = string.Join(",", request.CertificateAssetIds);
            profile.TrangThaiKYC = "Pending"; // Đổi trạng thái sang chờ duyệt
            profile.NgayCapNhat = DateTime.Now;

            // Cập nhật thông tin ngân hàng và mã số thuế nếu có
            if (!string.IsNullOrEmpty(request.BankInfo))
                profile.ThongTinNganHangMaHoa = request.BankInfo; // TODO: Mã hóa trong thực tế
            if (!string.IsNullOrEmpty(request.TaxCode))
                profile.MaSoThueMaHoa = request.TaxCode;

            await _context.SaveChangesAsync();

            return new OkObjectResult(
                new { message = "Hồ sơ KYC đã được gửi thành công, vui lòng đợi kiểm duyệt." }
            );
        }

        public async Task<IActionResult> GetKycStatusAsync(Guid instructorId)
        {
            var profile = await _context
                .HoSoGiangViens.Select(h => new
                {
                    h.MaGiangVien,
                    h.TrangThaiKYC,
                    h.LyDoTuChoi,
                    h.NgayCapNhat,
                })
                .FirstOrDefaultAsync(h => h.MaGiangVien == instructorId);

            if (profile == null)
                return new NotFoundObjectResult(
                    new { message = "Không tìm thấy hồ sơ giảng viên." }
                );

            return new OkObjectResult(profile);
        }
    }
}
