using System.Threading.Tasks;

namespace backend.Repository.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
        Task SendOtpEmailAsync(string to, string otp, string userName);
        Task SendResetPasswordEmailAsync(string to, string token, string userName);
    }
}
