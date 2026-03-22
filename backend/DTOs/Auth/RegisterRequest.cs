using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Auth
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải từ 6 ký tự trở lên.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Họ tên là bắt buộc.")]
        [MaxLength(200, ErrorMessage = "Họ tên không được quá 200 ký tự.")]
        public string HoTen { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vai trò là bắt buộc.")]
        public string Role { get; set; } = string.Empty; // "Student" hoặc "Instructor"
    }
}
