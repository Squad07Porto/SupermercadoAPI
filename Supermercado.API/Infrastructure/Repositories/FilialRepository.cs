using System.Data;
using Dapper;
using Supermercado.API.Infrastructure.Repositories.Interfaces;
using Supermercado.API.Models;

namespace Supermercado.API.Infrastructure.Repositories
{
    public class FilialRepository(IDbConnection connection) : IFilialRepository
    {
        private readonly IDbConnection _dbConnection = connection;

        public Task<IEnumerable<Filial>> GetAll()
        {
            using var connection = _dbConnection;
            return connection.QueryAsync<Filial>("SELECT * FROM Filial");
        }

        public Task<Filial?> GetById(int id)
        {
            using var connection = _dbConnection;
            return connection.QueryFirstOrDefaultAsync<Filial>("SELECT * FROM Filial WHERE Id = @Id", new { Id = id });
        }

        public Task Add(Filial filial)
        {
            using var connection = _dbConnection;
            var query = "INSERT INTO Filial (Nome, Endereco, CNPJ) VALUES (@Nome, @Endereco, @CNPJ)";
            return connection.ExecuteAsync(query, filial);
        }

        public Task Update(Filial filial)
        {
            using var connection = _dbConnection;
            var query = "UPDATE Filial SET Nome = @Nome, Endereco = @Endereco, CNPJ = @CNPJ WHERE Id = @Id";
            return connection.ExecuteAsync(query, filial);
        }

        public Task Delete(int id)
        {
            using var connection = _dbConnection;
            var query = "DELETE FROM Filial WHERE Id = @Id";
            return connection.ExecuteAsync(query, new { Id = id });
        }
    }
}