using Supermercado.API.Infrastructure.Repositories.Interfaces;
using Supermercado.API.Models;
using Supermercado.API.Services.Interfaces;

namespace Supermercado.API.Services
{
    public class FuncionarioService(IFuncionarioRepository funcionarioRepository) : IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository = funcionarioRepository;

        public async Task<IEnumerable<Funcionario>> GetAll()
        {
            return await _funcionarioRepository.GetAll();
        }

        public async Task<Funcionario?> GetById(int id)
        {
            return await _funcionarioRepository.GetById(id);
        }

        public async Task Add(Funcionario funcionario)
        {
            await _funcionarioRepository.Add(funcionario);
        }

        public async Task Update(Funcionario funcionario)
        {
            await _funcionarioRepository.Update(funcionario);
        }

        public async Task Delete(int id)
        {
            await _funcionarioRepository.Delete(id);
        }
    }
}