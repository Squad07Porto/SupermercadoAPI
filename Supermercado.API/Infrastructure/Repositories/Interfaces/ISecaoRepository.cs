using Supermercado.API.Models;

namespace Supermercado.API.Infrastructure.Repositories.Interfaces
{
    public interface ISecaoRepository
    {
        Task<IEnumerable<Secao>> GetAll();
        Task<Secao?> GetById(int id);
        Task Add(Secao secao);
        Task Update(Secao secao);
        Task Delete(int id);
    }
}