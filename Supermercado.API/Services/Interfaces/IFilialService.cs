using Supermercado.API.Models;

namespace Supermercado.API.Services.Interfaces
{
    public interface IFilialService
    {
        Task<IEnumerable<Filial>> GetAll();
        Task<Filial?> GetById(int id);
        Task Add(Filial filial);
        Task Update(Filial filial);
        Task Delete(int id);
    }
}