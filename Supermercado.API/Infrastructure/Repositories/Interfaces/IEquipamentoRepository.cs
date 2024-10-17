using Supermercado.API.Models;

namespace Supermercado.API.Infrastructure.Repositories.Interfaces
{
    public interface IEquipamentoRepository
    {
        Task<IEnumerable<Equipamento>> GetAll();
        Task<Equipamento?> GetById(int id);
        Task Add(Equipamento equipamento);
        Task Update(Equipamento equipamento);
        Task Delete(int id);
    }
}