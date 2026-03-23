using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace backend.Repository.Repositories
{
    public class EmailService : Services.IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");

            var email = new MimeMessage();
            email.From.Add(
                new MailboxAddress(smtpSettings["SenderName"]!, smtpSettings["SenderEmail"]!)
            );
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = body };
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(
                smtpSettings["Server"]!,
                int.Parse(smtpSettings["Port"]!),
                SecureSocketOptions.StartTls
            );
            await smtp.AuthenticateAsync(smtpSettings["Username"], smtpSettings["Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        public async Task SendOtpEmailAsync(string to, string otp, string userName)
        {
            var subject = "Xác thực Email - AET Academy";
            var body =
                $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 10px;'>
                    <h2 style='color: #007bff; text-align: center;'>Chào mừng {userName} đến với AET Academy!</h2>
                    <p>Cảm ơn bạn đã đăng ký tài khoản. Để hoàn tất quy trình xác thực, vui lòng nhập mã OTP bên dưới:</p>
                    <div style='background-color: #f8f9fa; padding: 15px; text-align: center; border-radius: 5px; margin: 20px 0;'>
                        <span style='font-size: 24px; font-weight: bold; letter-spacing: 5px; color: #333;'>{otp}</span>
                    </div>
                    <p>Mã này sẽ hết hạn sau 15 phút. Nếu bạn không yêu cầu mã này, vui lòng bỏ qua email này.</p>
                    <hr style='border: 0; border-top: 1px solid #eeeeee; margin: 20px 0;'>
                    <p style='font-size: 12px; color: #777; text-align: center;'>Đây là email tự động, vui lòng không trả lời.</p>
                </div>";

            await SendEmailAsync(to, subject, body);
        }

        public async Task SendResetPasswordEmailAsync(string to, string token, string userName)
        {
            var subject = "Đặt lại mật khẩu - AET Academy";
            var body =
                $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 10px;'>
                    <h2 style='color: #dc3545; text-align: center;'>Yêu cầu đặt lại mật khẩu</h2>
                    <p>Chào {userName}, chúng tôi nhận được yêu cầu đặt lại mật khẩu cho tài khoản của bạn.</p>
                    <p>Vui lòng sử dụng mã xác nhận bên dưới để đặt lại mật khẩu:</p>
                    <div style='background-color: #f8f9fa; padding: 15px; text-align: center; border-radius: 5px; margin: 20px 0;'>
                        <span style='font-size: 24px; font-weight: bold; letter-spacing: 5px; color: #333;'>{token}</span>
                    </div>
                    <p>Mã này sẽ hết hạn sau 15 phút. Nếu bạn không yêu cầu đặt lại mật khẩu, hãy bỏ qua email này và đổi mật khẩu để bảo vệ tài khoản.</p>
                    <hr style='border: 0; border-top: 1px solid #eeeeee; margin: 20px 0;'>
                    <p style='font-size: 12px; color: #777; text-align: center;'>Đây là email tự động, vui lòng không trả lời.</p>
                </div>";

            await SendEmailAsync(to, subject, body);
        }
    }
}
