using Supermercado.API.Infrastructure.Repositories.Interfaces;
using Supermercado.API.Models;
using Supermercado.API.Services.Interfaces;

namespace Supermercado.API.Services
{
    public class ClienteService(IClienteRepository clienteRepository) : IClienteService
    {
        private readonly IClienteRepository _clienteRepository = clienteRepository;

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            return await _clienteRepository.GetAll();
        }

        public async Task<Cliente?> GetById(int id)
        {
            return await _clienteRepository.GetById(id);
        }

        public async Task Add(Cliente cliente)
        {
            await _clienteRepository.Add(cliente);
        }

        public async Task Update(Cliente cliente)
        {
            await _clienteRepository.Update(cliente);
        }

        public async Task Delete(int id)
        {
            await _clienteRepository.Delete(id);
        }
    }
}