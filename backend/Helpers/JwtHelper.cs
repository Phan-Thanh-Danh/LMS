using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Models;
using Microsoft.IdentityModel.Tokens;

namespace backend.Helpers
{
    public static class JwtHelper
    {
        public static string GenerateAccessToken(NguoiDung user, IEnumerable<string> userRoles, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey =
                jwtSettings["SecretKey"] ?? "MộtCáiKeyThậtLàDàiVàBảoMậtĐểTránhLỗiHS256!!!";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.MaNguoiDung.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.HoTen),
                new Claim("isEmployee", user.LaNhanVien.ToString()),
            };

            // Thêm các vai trò vào Claims
            if (userRoles != null)
            {
                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    double.Parse(jwtSettings["AccessTokenExpirationMinutes"] ?? "15")
                ),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
