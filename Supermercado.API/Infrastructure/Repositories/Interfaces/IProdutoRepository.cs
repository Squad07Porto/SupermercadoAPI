using Supermercado.API.Models;

namespace Supermercado.API.Infrastructure.Repositories.Interfaces
{
  public interface IProdutoRepository
  {
    Task<IEnumerable<Produto>> GetAll();
    Task<Produto?> GetById(int id);
    Task Add(Produto produto);
    Task Update(Produto produto);
    Task Delete(int id);
  }
}
