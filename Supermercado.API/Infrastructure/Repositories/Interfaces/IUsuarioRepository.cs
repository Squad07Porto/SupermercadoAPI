using Supermercado.API.Models;

namespace Supermercado.API.Infrastructure.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        public Task<int> CreateUserAsync(Usuario usuario);
        public Task<Usuario?> GetUserByEmailAsync(string email);
    }
}