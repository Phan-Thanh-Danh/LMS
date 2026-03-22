using BCrypt.Net;

namespace backend.Helpers
{
    public static class PasswordHelper
    {
        /// <summary>
        /// Tạo muối (salt) ngẫu nhiên.
        /// </summary>
        public static string GenerateSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt();
        }

        /// <summary>
        /// Băm mật khẩu phối hợp với muối.
        /// </summary>
        public static string HashPassword(string password, string salt)
        {
            // Kết hợp mật khẩu và muối trước khi băm để tận dụng cột MuoiMatKhau trong DB
            return BCrypt.Net.BCrypt.HashPassword(password + salt);
        }

        /// <summary>
        /// Xác minh mật khẩu.
        /// </summary>
        public static bool VerifyPassword(string password, string salt, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password + salt, hashedPassword);
        }
    }
}
