using System;
using System.Threading.Tasks;
using backend.DTOs.Instructor;
using backend.Repository.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/moderator")]
    [Authorize(Roles = "Admin,Moderator")]
    public class ModeratorController : ControllerBase
    {
        private readonly IModeratorRepository _moderatorRepo;

        public ModeratorController(IModeratorRepository moderatorRepo)
        {
            _moderatorRepo = moderatorRepo;
        }

        [HttpGet("kyc")]
        public async Task<IActionResult> GetAllKyc([FromQuery] string? status)
        {
            return await _moderatorRepo.GetAllKycAsync(status);
        }

        [HttpGet("instructors/approved")]
        public async Task<IActionResult> GetApprovedInstructors()
        {
            return await _moderatorRepo.GetApprovedInstructorsAsync();
        }

        [HttpGet("kyc/{instructorId}")]
        public async Task<IActionResult> GetKycDetails(Guid instructorId)
        {
            return await _moderatorRepo.GetKycDetailsAsync(instructorId);
        }

        [HttpPost("kyc/{instructorId}/review")]
        public async Task<IActionResult> ReviewKyc(Guid instructorId, [FromBody] KycReviewDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return await _moderatorRepo.ReviewKycAsync(instructorId, request);
        }
    }
}
