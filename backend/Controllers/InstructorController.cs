using System;
using System.Security.Claims;
using System.Threading.Tasks;
using backend.DTOs.Instructor;
using backend.Repository.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/instructor")]
    [Authorize(Roles = "Instructor")]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorRepository _instructorRepo;

        public InstructorController(IInstructorRepository instructorRepo)
        {
            _instructorRepo = instructorRepo;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var profile = await _instructorRepo.GetProfileAsync(userId.Value);
            return Ok(profile);
        }

        [HttpPost("kyc")]
        public async Task<IActionResult> SubmitKyc([FromBody] KycSubmissionDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            return await _instructorRepo.SubmitKycAsync(userId.Value, request);
        }

        [HttpGet("kyc-status")]
        public async Task<IActionResult> GetKycStatus()
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            return await _instructorRepo.GetKycStatusAsync(userId.Value);
        }

        private Guid? GetCurrentUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(claim, out var id) ? id : null;
        }
    }
}
