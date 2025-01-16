using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APIPCHY_PhanQuyen.Models.QLKC.HT_NGUOIDUNG;
using System;

namespace APIPCHY_PhanQuyen.Services
{
    public class TokenService
    {
        private readonly string _secret;
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            // Lấy 'secret' từ appsettings.json
            _secret = configuration["JwtSettings:Secret"];
            _configuration = configuration;
        }

        public string GenerateJwtToken(HT_NGUOIDUNG_Model account)
        {
            // Tạo key từ secret
            var key = Encoding.UTF8.GetBytes(_secret);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, account.ten_dang_nhap),
                    new Claim(ClaimTypes.Dns, account.dm_phongban_id)
                }),
                Expires = DateTime.UtcNow.AddMinutes(20), // Token sẽ hết hạn sau 20 phút
                Issuer = _configuration["JwtSettings:Issuer"], // Lấy Issuer từ cấu hình
                Audience = _configuration["JwtSettings:Audience"], // Lấy Audience từ cấu hình
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token); // Trả về token dạng chuỗi
        }
    }
}
