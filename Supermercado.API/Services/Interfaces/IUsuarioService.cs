using System.Security.Claims;
using Supermercado.API.Models;

namespace Supermercado.API.Services.Interfaces
{
    public interface IUsuarioService
    {
        public Task RegisterUserAsync(Usuario usuario, ClaimsPrincipal userClaims);
        public Task<string?> AuthenticateUserAsync(Usuario usuario);
    }
}