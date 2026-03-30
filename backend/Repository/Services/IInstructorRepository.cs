using System;
using System.Threading.Tasks;
using backend.DTOs.Instructor;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Repository.Services
{
    public interface IInstructorRepository
    {
        Task<HoSoGiangVien?> GetProfileAsync(Guid instructorId);
        Task<IActionResult> SubmitKycAsync(Guid instructorId, KycSubmissionDto request);
        Task<IActionResult> GetKycStatusAsync(Guid instructorId);
    }
}
