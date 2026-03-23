using System.Collections.Generic;

namespace backend.DTOs.User
{
    public class UpdateUserRequest
    {
        public string? HoTen { get; set; }
        public string? DuongDanAnhDaiDien { get; set; }
        public string? TieuSu { get; set; }
        public List<int>? RoleIds { get; set; }
    }
}
