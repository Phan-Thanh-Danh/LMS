namespace backend.DTOs.Role
{
    public class RoleResponse
    {
        public int MaVaiTro { get; set; }
        public string TenVaiTro { get; set; } = string.Empty;
        public string? MoTa { get; set; }
        public bool DangHoatDong { get; set; }
        public int SoNguoiDung { get; set; }
    }
}
