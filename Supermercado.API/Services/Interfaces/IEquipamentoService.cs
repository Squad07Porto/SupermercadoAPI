using Supermercado.API.Models;

namespace Supermercado.API.Services.Interfaces
{
    public interface IEquipamentoService
    {
        Task<IEnumerable<Equipamento>> GetAll();
        Task<Equipamento?> GetById(int id);
        Task Add(Equipamento equipamento);
        Task Update(Equipamento equipamento);
        Task Delete(int id);
    }
}