using Supermercado.API.Infrastructure.Repositories.Interfaces;
using Supermercado.API.Models;
using Supermercado.API.Services.Interfaces;

namespace Supermercado.API.Services
{
    public class FilialService(IFilialRepository repository) : IFilialService
    {
        private readonly IFilialRepository _repository = repository;

        public Task<IEnumerable<Filial>> GetAll()
        {
            return _repository.GetAll();
        }

        public Task<Filial?> GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Task Add(Filial filial)
        {
            return _repository.Add(filial);
        }

        public Task Update(Filial filial)
        {
            return _repository.Update(filial);
        }

        public Task Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}