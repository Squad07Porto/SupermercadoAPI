using Supermercado.API.Models;

namespace Supermercado.API.Infrastructure.Repositories.Interfaces
{
    public interface IFuncionarioRepository
    {
        Task<IEnumerable<Funcionario>> GetAll();
        Task<Funcionario?> GetById(int id);
        Task Add(Funcionario funcionario);
        Task Update(Funcionario funcionario);
        Task Delete(int id);
    }
}