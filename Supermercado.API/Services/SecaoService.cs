using Supermercado.API.Infrastructure.Repositories.Interfaces;
using Supermercado.API.Models;
using Supermercado.API.Services.Interfaces;

namespace Supermercado.API.Services
{
    public class SecaoService(ISecaoRepository secaoRepository) : ISecaoService
    {
        private readonly ISecaoRepository _secaoRepository = secaoRepository;

        public async Task<IEnumerable<Secao>> GetAll()
        {
            return await _secaoRepository.GetAll();
        }

        public async Task<Secao?> GetById(int id)
        {
            return await _secaoRepository.GetById(id);
        }

        public async Task Add(Secao secao)
        {
            await _secaoRepository.Add(secao);
        }

        public async Task Update(Secao secao)
        {
            await _secaoRepository.Update(secao);
        }

        public async Task Delete(int id)
        {
            await _secaoRepository.Delete(id);
        }
    }
}