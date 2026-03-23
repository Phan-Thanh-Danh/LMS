namespace backend.DTOs.Auth
{
    public class VerifyOtpRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // EmailVerify | PasswordReset
    }
}
