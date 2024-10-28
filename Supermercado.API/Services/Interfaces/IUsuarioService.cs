using Supermercado.API.Models;

namespace Supermercado.API.Services.Interfaces
{
    public interface IUsuarioService
    {
        public Task RegisterUserAsync(Usuario usuario);
        public Task<string?> AuthenticateUserAsync(Usuario usuario);
    }
}