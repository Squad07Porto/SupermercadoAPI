using Supermercado.API.Models;

namespace Supermercado.API.Infrastructure.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetAll();
        Task<Cliente?> GetById(int id);
        Task Add(Cliente cliente);
        Task Update(Cliente cliente);
        Task Delete(int id);
    }
}