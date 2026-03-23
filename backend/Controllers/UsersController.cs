using System;
using System.Threading.Tasks;
using backend.DTOs.User;
using backend.Repository.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize] // Tất cả endpoint đều cần đăng nhập
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Admin, CS và Moderator đều có thể xem danh sách
        [HttpGet]
        [Authorize(Roles = "Admin,CS,Moderator")]
        public async Task<IActionResult> GetAllWithRoles([FromQuery] UserQueryParameters query)
        {
            return await _userRepository.GetAllWithRolesAsync(query);
        }

        // Admin, CS và Moderator đều có thể xem chi tiết
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,CS,Moderator")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        // Chỉ Admin và CS mới được tạo tài khoản mới
        [HttpPost]
        [Authorize(Roles = "Admin,CS")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return await _userRepository.CreateUserAsync(request);
        }

        // Chỉ Admin và CS mới được sửa thông tin tài khoản
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,CS")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return await _userRepository.UpdateUserAsync(id, request);
        }

        // Admin và Moderator mới được khóa/mở khóa tài khoản
        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> ToggleActiveStatus(Guid id)
        {
            return await _userRepository.ToggleActiveStatusAsync(id);
        }

        // Chỉ Admin mới được xóa mềm
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            return await _userRepository.SoftDeleteAsync(id);
        }

        // Chỉ Admin mới được xóa cứng — chỉ áp dụng khi email chưa xác thực và chưa có giao dịch
        [HttpDelete("{id}/permanent")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> HardDelete(Guid id)
        {
            return await _userRepository.HardDeleteAsync(id);
        }
    }
}
