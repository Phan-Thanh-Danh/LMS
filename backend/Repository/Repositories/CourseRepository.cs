using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.DTOs.Course;
using backend.Models;
using backend.Repository.Services;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // ── Khóa học ──────────────────────────────────────────────

        public async Task<KhoaHoc?> GetCourseByIdAsync(Guid id) =>
            await _context
                .KhoaHocs.Include(k => k.GiangVien)
                .Include(k => k.DanhMuc)
                .FirstOrDefaultAsync(k => k.MaKhoaHoc == id && !k.DaXoa);

        public async Task<KhoaHoc?> GetCourseBySlugAsync(string slug) =>
            await _context
                .KhoaHocs.Include(k => k.GiangVien)
                .Include(k => k.DanhMuc)
                .FirstOrDefaultAsync(k => k.DuongDanURL == slug && !k.DaXoa);

        public async Task<List<KhoaHoc>> GetAllCoursesAsync(int? trangThai = null)
        {
            var query = _context
                .KhoaHocs.Include(k => k.GiangVien)
                .Include(k => k.DanhMuc)
                .Where(k => !k.DaXoa);

            if (trangThai.HasValue)
                query = query.Where(k => k.TrangThai == trangThai.Value);

            return await query.OrderByDescending(k => k.NgayTao).ToListAsync();
        }

        public async Task<List<KhoaHoc>> GetCoursesByInstructorAsync(Guid instructorId) =>
            await _context
                .KhoaHocs.Include(k => k.DanhMuc)
                .Where(k => k.MaGiangVien == instructorId && !k.DaXoa)
                .OrderByDescending(k => k.NgayTao)
                .ToListAsync();

        public async Task<List<KhoaHoc>> GetPendingCoursesAsync() =>
            await _context
                .KhoaHocs.Include(k => k.GiangVien)
                .Include(k => k.DanhMuc)
                .Where(k => k.TrangThai == 1 && !k.DaXoa)
                .OrderBy(k => k.NgayTao) // FIFO - duyệt theo thứ tự gửi
                .ToListAsync();

        public async Task<List<KhoaHoc>> GetPublishedCoursesAsync() =>
            await _context
                .KhoaHocs.Include(k => k.GiangVien)
                .Include(k => k.DanhMuc)
                .Where(k => k.TrangThai == 2 && !k.DaXoa)
                .OrderByDescending(k => k.XuatBanLuc)
                .ToListAsync();

        public async Task<List<KhoaHoc>> AdminSearchCoursesAsync(string? keyword, int? trangThai, Guid? instructorId)
        {
            var query = _context.KhoaHocs
                .Include(k => k.GiangVien)
                .Include(k => k.DanhMuc)
                .Where(k => !k.DaXoa)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var lowerKeyword = keyword.ToLower();
                query = query.Where(k => k.TieuDe.ToLower().Contains(lowerKeyword) || (k.GiangVien.HoTen != null && k.GiangVien.HoTen.ToLower().Contains(lowerKeyword)));
            }

            if (trangThai.HasValue)
            {
                query = query.Where(k => k.TrangThai == trangThai.Value);
            }

            if (instructorId.HasValue)
            {
                query = query.Where(k => k.MaGiangVien == instructorId.Value);
            }

            return await query.OrderByDescending(k => k.NgayTao).ToListAsync();
        }


        public async Task AddCourseAsync(KhoaHoc course)
        {
            _context.KhoaHocs.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCourseAsync(KhoaHoc course)
        {
            course.NgayCapNhat = DateTime.Now;
            _context.KhoaHocs.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteCourseAsync(Guid id)
        {
            var course = await _context.KhoaHocs.FindAsync(id);
            if (course == null)
                return false;
            course.DaXoa = true;
            course.NgayCapNhat = DateTime.Now;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> IsSlugUniqueAsync(string slug, Guid? excludeId = null)
        {
            var query = _context.KhoaHocs.Where(k => k.DuongDanURL == slug && !k.DaXoa);
            if (excludeId.HasValue)
                query = query.Where(k => k.MaKhoaHoc != excludeId.Value);
            return !await query.AnyAsync();
        }

        // ── Quản lý vòng đời trạng thái ──────────────────────────

        public async Task<bool> SubmitForReviewAsync(Guid courseId)
        {
            var course = await _context.KhoaHocs.FindAsync(courseId);
            if (course == null || course.TrangThai != 0)
                return false;
            course.TrangThai = 1; // Pending
            course.NgayCapNhat = DateTime.Now;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ApproveCourseAsync(Guid courseId)
        {
            var course = await _context.KhoaHocs.FindAsync(courseId);
            if (course == null || course.TrangThai != 1)
                return false;
            course.TrangThai = 2; // Published
            course.XuatBanLuc = DateTime.Now;
            course.GhiChuTuChoi = null;
            course.NgayCapNhat = DateTime.Now;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RejectCourseAsync(Guid courseId, string reason)
        {
            var course = await _context.KhoaHocs.FindAsync(courseId);
            if (course == null || course.TrangThai != 1)
                return false;
            course.TrangThai = 3; // Rejected
            course.GhiChuTuChoi = reason;
            course.NgayCapNhat = DateTime.Now;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ArchiveCourseAsync(Guid courseId)
        {
            var course = await _context.KhoaHocs.FindAsync(courseId);
            if (course == null)
                return false;
            course.TrangThai = 4; // Archived
            course.LuuTruLuc = DateTime.Now;
            course.NgayCapNhat = DateTime.Now;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ToggleCourseStatusAsync(Guid courseId)
        {
            var course = await _context.KhoaHocs.FindAsync(courseId);
            if (course == null) return false;

            // Toggle between Draft (0) and Published (2) assuming simple toggle, or Unpublish
            if (course.TrangThai == 2)
                course.TrangThai = 0; // Unpublish -> Draft
            else if (course.TrangThai == 0 || course.TrangThai == 3 || course.TrangThai == 4)
                course.TrangThai = 2; // Publish

            course.NgayCapNhat = DateTime.Now;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> HasEnrollmentsAsync(Guid courseId)
        {
            return await _context.GhiDanhs.AnyAsync(g => g.MaKhoaHoc == courseId);
        }

        public async Task<bool> HardDeleteCourseAsync(Guid courseId, string webRootPath)
        {
            // Begin transaction for safety
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var course = await _context.KhoaHocs
                    .Include(c => c.Chuongs)
                        .ThenInclude(ch => ch.BaiGiangs)
                            .ThenInclude(b => b.TaiNguyenSo)
                    .FirstOrDefaultAsync(c => c.MaKhoaHoc == courseId);

                if (course == null) return false;

                // Collect all files to delete
                var filesToDelete = new List<string>();
                
                // Add course thumbnail to delete list if it's local
                if (!string.IsNullOrEmpty(course.DuongDanAnhDaiDien) && !course.DuongDanAnhDaiDien.StartsWith("http"))
                {
                    filesToDelete.Add(System.IO.Path.Combine(webRootPath, course.DuongDanAnhDaiDien.TrimStart('/')));
                }

                foreach (var chuong in course.Chuongs)
                {
                    foreach (var baiGiang in chuong.BaiGiangs)
                    {
                        if (baiGiang.TaiNguyenSo != null)
                        {
                            var resourceFile = System.IO.Path.Combine(webRootPath, baiGiang.TaiNguyenSo.DuongDanLuuTru.TrimStart('/'));
                            filesToDelete.Add(resourceFile);
                            _context.TaiNguyenSos.Remove(baiGiang.TaiNguyenSo);
                        }
                    }
                }

                // Delete the course (cascading will delete Chuong and BaiGiang if configured, otherwise remove them explicitly)
                _context.KhoaHocs.Remove(course);
                await _context.SaveChangesAsync();
                
                await transaction.CommitAsync();

                // Delete physical files after successful DB transaction
                foreach (var file in filesToDelete)
                {
                    if (System.IO.File.Exists(file))
                    {
                        try { System.IO.File.Delete(file); } catch { /* Log error potentially */ }
                    }
                }

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        // ── Chương (Sections) ─────────────────────────────────────

        public async Task<Chuong?> GetSectionByIdAsync(int id) =>
            await _context.Chuongs
                .Include(c => c.KhoaHoc)
                .FirstOrDefaultAsync(c => c.MaChuong == id);

        public async Task<List<Chuong>> GetSectionsByCourseAsync(Guid courseId) =>
            await _context
                .Chuongs.Where(c => c.MaKhoaHoc == courseId)
                .OrderBy(c => c.ThuTu)
                .ToListAsync();

        public async Task<List<Chuong>> GetFullCurriculumTreeAsync(Guid courseId) =>
            await _context.Chuongs
                .Include(c => c.BaiGiangs)
                    .ThenInclude(bg => bg.TaiNguyenSo)
                .Where(c => c.MaKhoaHoc == courseId)
                .OrderBy(c => c.ThuTu)
                .ToListAsync();

        public async Task AddSectionAsync(Chuong section)
        {
            _context.Chuongs.Add(section);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSectionAsync(Chuong section)
        {
            _context.Chuongs.Update(section);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteSectionAsync(int id)
        {
            var section = await _context.Chuongs.FindAsync(id);
            if (section == null)
                return false;
            _context.Chuongs.Remove(section);
            return await _context.SaveChangesAsync() > 0;
        }

        // ── Bài giảng (Lectures) ──────────────────────────────────

        public async Task<BaiGiang?> GetLectureByIdAsync(int id) =>
            await _context
                .BaiGiangs.Include(b => b.TaiNguyenSo)
                .FirstOrDefaultAsync(b => b.MaBaiGiang == id);

        public async Task<List<BaiGiang>> GetLecturesBySectionAsync(int sectionId) =>
            await _context
                .BaiGiangs.Include(b => b.TaiNguyenSo)
                .Where(b => b.MaChuong == sectionId)
                .OrderBy(b => b.ThuTu)
                .ToListAsync();

        public async Task AddLectureAsync(BaiGiang lecture)
        {
            _context.BaiGiangs.Add(lecture);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLectureAsync(BaiGiang lecture)
        {
            _context.BaiGiangs.Update(lecture);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteLectureAsync(int id)
        {
            var lecture = await _context.BaiGiangs.FindAsync(id);
            if (lecture == null)
                return false;
            _context.BaiGiangs.Remove(lecture);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AttachMediaToLectureAsync(int lectureId, Guid mediaId)
        {
            var lecture = await _context.BaiGiangs.FindAsync(lectureId);
            var media = await _context.TaiNguyenSos.FindAsync(mediaId);
            if (lecture == null || media == null)
                return false;

            lecture.MaTaiNguyen = mediaId;
            if (media.ThoiLuong.HasValue)
                lecture.ThoiLuong = media.ThoiLuong;

            return await _context.SaveChangesAsync() > 0;
        }

        // ── Tài nguyên số (Media) ─────────────────────────────────

        public async Task<TaiNguyenSo?> GetMediaByIdAsync(Guid id) =>
            await _context.TaiNguyenSos.FindAsync(id);

        public async Task AddMediaAsync(TaiNguyenSo media)
        {
            _context.TaiNguyenSos.Add(media);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMediaStatusAsync(Guid mediaId, string status)
        {
            var media = await _context.TaiNguyenSos.FindAsync(mediaId);
            if (media != null)
            {
                media.TrangThai = status;
                await _context.SaveChangesAsync();
            }
        }
    }
}
