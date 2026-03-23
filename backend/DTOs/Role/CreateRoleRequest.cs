using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Role
{
    public class CreateRoleRequest
    {
        [Required]
        [MaxLength(100)]
        public string TenVaiTro { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? MoTa { get; set; }
    }
}
