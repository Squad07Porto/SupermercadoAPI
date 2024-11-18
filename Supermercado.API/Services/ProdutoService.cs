using Supermercado.API.Infrastructure.Repositories.Interfaces;
using Supermercado.API.Models;
using Supermercado.API.Services.Interfaces;

namespace Supermercado.API.Services
{
    public class ProdutoService(IProdutoRepository produtoRepository) : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _produtoRepository.GetAll();
        }

        public async Task<Produto?> GetById(int id)
        {
            return await _produtoRepository.GetById(id);
        }

        public async Task Add(Produto produto)
        {
            await _produtoRepository.Add(produto);
        }

        public async Task Update(Produto produto)
        {
            await _produtoRepository.Update(produto);
        }

        public async Task Delete(int id)
        {
            await _produtoRepository.Delete(id);
        }

        public async Task<IEnumerable<Produto>> GetBySecaoId(int secaoId)
        {
            return await _produtoRepository.GetBySecaoId(secaoId);
        }

        public async Task<IEnumerable<Produto>> GetByFilialId(int filialId)
        {
            return await _produtoRepository.GetByFilialId(filialId);
        }
    }
}
