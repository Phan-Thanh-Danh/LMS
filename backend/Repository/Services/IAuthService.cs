using backend.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace backend.Repository.Services
{
    public interface IAuthService
    {
        Task<IActionResult> RegisterAsync(RegisterRequest request);
        Task<IActionResult> LoginAsync(LoginRequest request);
    }
}
