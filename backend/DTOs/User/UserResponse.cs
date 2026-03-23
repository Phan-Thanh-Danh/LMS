using System;
using System.Collections.Generic;

namespace backend.DTOs.User
{
    public class UserResponse
    {
        public Guid MaNguoiDung { get; set; }
        public string Email { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public string? DuongDanAnhDaiDien { get; set; }
        public string? TieuSu { get; set; }
        public bool DangHoatDong { get; set; }
        public bool EmailDaXacThuc { get; set; }
        public bool LaNhanVien { get; set; }
        public DateTime NgayTao { get; set; }
        public List<int> Roles { get; set; } = new List<int>();
    }
}
