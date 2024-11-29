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
            string? secretKey = Environment.GetEnvironmentVariable("JWT_SECRET");
            string? issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
            string? audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

            if (string.IsNullOrWhiteSpace(secretKey) || string.IsNullOrWhiteSpace(issuer) || string.IsNullOrWhiteSpace(audience))
            {
                throw new Exception("JWT configuration is missing. Please set JWT_SECRET, JWT_ISSUER, and JWT_AUDIENCE environment variables.");
            }

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