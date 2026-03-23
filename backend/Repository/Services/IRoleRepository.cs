using System.Threading.Tasks;
using backend.DTOs.Role;
using Microsoft.AspNetCore.Mvc;

namespace backend.Repository.Services
{
    public interface IRoleRepository
    {
        Task<IActionResult> GetAllAsync();
        Task<IActionResult> GetByIdAsync(int id);
        Task<IActionResult> CreateAsync(CreateRoleRequest request);
        Task<IActionResult> UpdateAsync(int id, UpdateRoleRequest request);
        Task<IActionResult> ToggleActiveStatusAsync(int id);
        Task<IActionResult> HardDeleteAsync(int id);
    }
}
