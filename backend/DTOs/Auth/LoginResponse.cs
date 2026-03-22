namespace backend.DTOs.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public UserDto User { get; set; } = new UserDto();
    }

    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public List<RoleDto> Roles { get; set; } = new List<RoleDto>();
    }

    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
