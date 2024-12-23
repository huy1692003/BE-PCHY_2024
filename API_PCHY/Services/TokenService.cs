using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using APIPCHY_PhanQuyen.Models.QLKC.HT_NGUOIDUNG;

namespace APIPCHY_PhanQuyen.Services
{
    public class TokenService
    {
        private readonly string secret;

        public TokenService(IConfiguration configuration)
        {
            // Lấy `secret` từ appsettings.json
            secret = configuration["JwtSettings:Secret"];
        }

        public string GenerateJwtToken(HT_NGUOIDUNG_Model account)
        {
            // Tạo `key` từ `secret`
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, account.ten_dang_nhap.ToString()),
                    new Claim(ClaimTypes.Gender, account.gioi_tinh.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(20), // Thời hạn token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
