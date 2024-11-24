using System.Data;
using Dapper;
using Supermercado.API.Infrastructure.Repositories.Interfaces;
using Supermercado.API.Models;

namespace Supermercado.API.Infrastructure.Repositories
{
    public class FilialRepository(IDbConnection connection) : IFilialRepository
    {
        private readonly IDbConnection _dbConnection = connection;

        public async Task<IEnumerable<Filial>> GetAll()
        {
            using var connection = _dbConnection;
            var query = "SELECT * FROM Filial";
            return await connection.QueryAsync<Filial>(query);
        }

        public async Task<Filial?> GetById(int id)
        {
            using var connection = _dbConnection;
            var query = "SELECT * FROM Filial WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Filial>(query, new { Id = id });
        }

        public async Task Add(Filial filial)
        {
            using var connection = _dbConnection;
            var query = "INSERT INTO Filial (Nome, Endereco, CNPJ) VALUES (@Nome, @Endereco, @CNPJ)";
            await connection.ExecuteAsync(query, filial);
        }

        public async Task Update(Filial filial)
        {
            using var connection = _dbConnection;
            var query = "UPDATE Filial SET Nome = @Nome, Endereco = @Endereco, CNPJ = @CNPJ WHERE Id = @Id";
            await connection.ExecuteAsync(query, filial);
        }

        public async Task Delete(int id)
        {
            using var connection = _dbConnection;
            var query = "DELETE FROM Filial WHERE Id = @Id";
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}