using backend.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace backend.Repository.Services
{
    public interface IAuthService
    {
        Task<IActionResult> RegisterAsync(RegisterRequest request);
        Task<IActionResult> LoginAsync(LoginRequest request);
        Task<IActionResult> VerifyOtpAsync(VerifyOtpRequest request);
        Task<IActionResult> ForgotPasswordAsync(ForgotPasswordRequest request);
        Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest request);
        Task<IActionResult> ResendOtpAsync(string email, string type);
        Task<IActionResult> ChangePasswordAsync(Guid userId, ChangePasswordRequest request);
        Task<IActionResult> GetProfileAsync(Guid userId);
        Task<IActionResult> UpdateProfileAsync(Guid userId, UpdateProfileRequest request);
        Task<IActionResult> LogoutAsync(LogoutRequest request);
    }
}
