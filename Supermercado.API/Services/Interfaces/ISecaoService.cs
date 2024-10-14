using Supermercado.API.Models;

namespace Supermercado.API.Services.Interfaces
{
    public interface ISecaoService
    {
        Task<IEnumerable<Secao>> GetAll();
        Task<Secao?> GetById(int id);
        Task Add(Secao secao);
        Task Update(Secao secao);
        Task Delete(int id);
    }
    
}