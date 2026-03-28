using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.DTOs.Course;
using backend.Models;

namespace backend.Repository.Services
{
    public interface ICourseRepository
    {
        // ── Khóa học ──────────────────────────────────────────────
        Task<KhoaHoc?> GetCourseByIdAsync(Guid id);
        Task<KhoaHoc?> GetCourseBySlugAsync(string slug);
        Task<List<KhoaHoc>> GetAllCoursesAsync(int? trangThai = null);
        Task<List<KhoaHoc>> GetCoursesByInstructorAsync(Guid instructorId);
        Task<List<KhoaHoc>> GetPendingCoursesAsync();
        Task<List<KhoaHoc>> GetPublishedCoursesAsync();
        Task AddCourseAsync(KhoaHoc course);
        Task UpdateCourseAsync(KhoaHoc course);
        Task<bool> DeleteCourseAsync(Guid id);
        Task<bool> IsSlugUniqueAsync(string slug, Guid? excludeId = null);

        // ── Quản lý vòng đời trạng thái ──────────────────────────
        Task<bool> SubmitForReviewAsync(Guid courseId);
        Task<bool> ApproveCourseAsync(Guid courseId);
        Task<bool> RejectCourseAsync(Guid courseId, string reason);
        Task<bool> ArchiveCourseAsync(Guid courseId);

        // ── Chương (Sections) ─────────────────────────────────────
        Task<Chuong?> GetSectionByIdAsync(int id);
        Task<List<Chuong>> GetSectionsByCourseAsync(Guid courseId);
        Task AddSectionAsync(Chuong section);
        Task UpdateSectionAsync(Chuong section);
        Task<bool> DeleteSectionAsync(int id);

        // ── Bài giảng (Lectures) ──────────────────────────────────
        Task<BaiGiang?> GetLectureByIdAsync(int id);
        Task<List<BaiGiang>> GetLecturesBySectionAsync(int sectionId);
        Task AddLectureAsync(BaiGiang lecture);
        Task UpdateLectureAsync(BaiGiang lecture);
        Task<bool> DeleteLectureAsync(int id);
        Task<bool> AttachMediaToLectureAsync(int lectureId, Guid mediaId);

        // ── Tài nguyên số (Media) ─────────────────────────────────
        Task<TaiNguyenSo?> GetMediaByIdAsync(Guid id);
        Task AddMediaAsync(TaiNguyenSo media);
        Task UpdateMediaStatusAsync(Guid mediaId, string status);
    }
}
