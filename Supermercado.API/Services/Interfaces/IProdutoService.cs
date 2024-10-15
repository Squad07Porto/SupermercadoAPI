using Supermercado.API.Models;

namespace Supermercado.API.Services.Interfaces
{
  public interface IProdutoService
  {
    Task<IEnumerable<Produto>> GetAll();
    Task<Produto?> GetById(int id);
    Task Add(Produto produto);
    Task Update(Produto produto);
    Task Delete(int id);
  }
}
