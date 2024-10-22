using Supermercado.API.Infrastructure.Repositories.Interfaces;
using Supermercado.API.Models;
using Supermercado.API.Services.Interfaces;

namespace Supermercado.API.Services
{
    public class EquipamentoService(IEquipamentoRepository equipamentoRepository) : IEquipamentoService
    {
        private readonly IEquipamentoRepository _equipamentoRepository = equipamentoRepository;

        public async Task<IEnumerable<Equipamento>> GetAll()
        {
            return await _equipamentoRepository.GetAll();
        }

        public async Task<Equipamento?> GetById(int id)
        {
            return await _equipamentoRepository.GetById(id);
        }

        public async Task Add(Equipamento equipamento)
        {
            await _equipamentoRepository.Add(equipamento);
        }

        public async Task Update(Equipamento equipamento)
        {
            await _equipamentoRepository.Update(equipamento);
        }

        public async Task Delete(int id)
        {
            await _equipamentoRepository.Delete(id);
        }
    }



}