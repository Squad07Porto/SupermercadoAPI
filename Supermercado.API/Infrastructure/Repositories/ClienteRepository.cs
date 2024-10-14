using System.Data;
using Dapper;
using Supermercado.API.Infrastructure.Repositories.Interfaces;
using Supermercado.API.Models;

namespace Supermercado.API.Infrastructure.Repositories
{
    public class ClienteRepository(IDbConnection dbConnection) : IClienteRepository
    {
        private readonly IDbConnection _dbConnection = dbConnection;

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            using var connection = _dbConnection;
            var query = "SELECT * FROM Cliente";
            return await connection.QueryAsync<Cliente>(query);
        }

        public async Task<Cliente?> GetById(int id)
        {
            using var connection = _dbConnection;
            var query = "SELECT * FROM Cliente WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Cliente>(query, new { Id = id });
        }

        public async Task Add(Cliente cliente)
        {
            using var connection = _dbConnection;
            var query = "INSERT INTO Cliente (Nome, Email) VALUES (@Nome, @Email)";
            await connection.ExecuteAsync(query, cliente);
        }

        public async Task Update(Cliente cliente)
        {
            using var connection = _dbConnection;
            var query = "UPDATE Cliente SET Nome = @Nome, Email = @Email WHERE Id = @Id";
            await connection.ExecuteAsync(query, cliente);
        }

        public async Task Delete(int id)
        {
            using var connection = _dbConnection;
            var query = "DELETE FROM Cliente WHERE Id = @Id";
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}