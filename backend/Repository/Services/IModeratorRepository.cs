using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.DTOs.Instructor;
using Microsoft.AspNetCore.Mvc;

namespace backend.Repository.Services
{
    public interface IModeratorRepository
    {
        Task<IActionResult> GetAllKycAsync(string? status);
        Task<IActionResult> GetApprovedInstructorsAsync();
        Task<IActionResult> GetKycDetailsAsync(Guid instructorId);
        Task<IActionResult> ReviewKycAsync(Guid instructorId, KycReviewDto request);
    }
}
