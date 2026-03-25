using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Auth
{
    public class LogoutRequest
    {
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
