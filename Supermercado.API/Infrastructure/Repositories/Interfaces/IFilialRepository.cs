using Supermercado.API.Models;

namespace Supermercado.API.Infrastructure.Repositories.Interfaces
{
    public interface IFilialRepository
    {
        public Task<IEnumerable<Filial>> GetAll();
        public Task<Filial?> GetById(int id);
        public Task Add(Filial filial);
        public Task Update(Filial filial);
        public Task Delete(int id);
    }
}