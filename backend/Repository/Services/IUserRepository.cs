using System;
using System.Threading.Tasks;
using backend.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace backend.Repository.Services
{
    public interface IUserRepository
    {
        Task<IActionResult> GetAllWithRolesAsync(UserQueryParameters query);
        Task<IActionResult> GetByIdAsync(Guid id);
        Task<IActionResult> CreateUserAsync(CreateUserRequest request);
        Task<IActionResult> UpdateUserAsync(Guid id, UpdateUserRequest request);
        Task<IActionResult> ToggleActiveStatusAsync(Guid id);
        Task<IActionResult> SoftDeleteAsync(Guid id);
        Task<IActionResult> HardDeleteAsync(Guid id);
    }
}
