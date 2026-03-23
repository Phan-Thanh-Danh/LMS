using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.User
{
    public class CreateUserRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string HoTen { get; set; } = string.Empty;

        public string? DuongDanAnhDaiDien { get; set; }
        public string? TieuSu { get; set; }

        // Mặc định tạo user mới sẽ có quyền Student (ví dụ: RoleId = 1)
        // Admin có thể truyền kèm danh sách các RoleId khác
        public List<int> RoleIds { get; set; } = new List<int>();
    }
}
