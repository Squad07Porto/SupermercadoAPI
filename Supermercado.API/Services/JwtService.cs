using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Supermercado.API.Services
{
    public class JwtService(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        public string GenerateToken(string email, string cargo)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            string secretKey = jwtSettings["SecretKey"]!;
            string issuer = jwtSettings["Issuer"]!;
            string audience = jwtSettings["Audience"]!;
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            var expiral = TimeZoneInfo.ConvertTime(DateTime.Now, timeZone);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims =
            [
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim("Cargo", cargo),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            ];

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiral.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}