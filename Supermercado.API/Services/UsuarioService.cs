using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Supermercado.API.Infrastructure.Repositories.Interfaces;
using Supermercado.API.Models;
using Supermercado.API.Services.Interfaces;

namespace Supermercado.API.Services
{
    public class UsuarioService(IUsuarioRepository usuarioRepository, JwtService jwtService) : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly JwtService _jwtService = jwtService;

        public async Task RegisterUserAsync(Usuario usuario, ClaimsPrincipal userClaims)
        {
            var cargoClaim = userClaims.Claims.FirstOrDefault(c => c.Type == "Cargo")?.Value.NormalizeString();

            if (cargoClaim == null || cargoClaim != "funcionario")
            {
                throw new UnauthorizedAccessException("Somente funcionários podem criar contas de funcionário.");
            }

            if (usuario.Cargo.NormalizeString() == "funcionario" && cargoClaim != "funcionario")
            {
                throw new UnauthorizedAccessException("Somente funcionários podem criar contas de funcionário.");
            }

            var salt = GenerateSalt();
            usuario.Salt = salt;
            usuario.Senha = HashPassword(usuario.Senha, salt);

            await _usuarioRepository.CreateUserAsync(usuario);
        }

        public async Task<string?> AuthenticateUserAsync(Usuario usuario)
        {
            var email = usuario.Email;
            var senha = usuario.Senha;

            var usuarioCadastrado = await _usuarioRepository.GetUserByEmailAsync(email);

            if (usuarioCadastrado == null || !ValidateUserPassword(senha, usuarioCadastrado.Senha, usuarioCadastrado.Salt))
            {
                return null;
            }

            return _jwtService.GenerateToken(usuarioCadastrado.Email, usuarioCadastrado.Cargo);
        }

        private static bool ValidateUserPassword(string senha, string hashedPassword, string salt)
        {
            var hashedInputPassword = HashPassword(senha, salt);
            return hashedInputPassword == hashedPassword;
        }

        private static string HashPassword(string senha, string salt)
        {
            var saltedPassword = senha + salt;
            var saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);
            var hashedPasswordBytes = SHA256.HashData(saltedPasswordBytes);
            return Convert.ToBase64String(hashedPasswordBytes);
        }

        private static string GenerateSalt()
        {
            var saltBytes = new byte[16];
            using var provider = RandomNumberGenerator.Create();
            provider.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }
    }
}
