namespace backend.DTOs.Auth
{
    public class UpdateProfileRequest
    {
        public string HoTen { get; set; } = string.Empty;
        public string? DuongDanAnhDaiDien { get; set; }
        public string? TieuSu { get; set; }
    }
}
