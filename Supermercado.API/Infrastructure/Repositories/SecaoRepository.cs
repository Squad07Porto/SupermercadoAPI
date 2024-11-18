using System.Data;
using Dapper;
using Supermercado.API.Infrastructure.Repositories.Interfaces;
using Supermercado.API.Models;

namespace Supermercado.API.Infrastructure.Repositories
{
    public class SecaoRepository(IDbConnection dbConnection) : ISecaoRepository
    {
        private readonly IDbConnection _dbConnection = dbConnection;

        public async Task<IEnumerable<Secao>> GetAll()
        {
            using var connection = _dbConnection;
            var query = "SELECT * FROM Secao";
            return await connection.QueryAsync<Secao>(query);
        }

        public async Task<Secao?> GetById(int id)
        {
            using var connection = _dbConnection;
            var query = "SELECT * FROM Secao WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Secao>(query, new { Id = id });
        }

        public async Task Add(Secao secao)
        {
            using var connection = _dbConnection;
            var query = "INSERT INTO Secao (Descricao, FilialId) VALUES (@Descricao, @FilialId)";
            await connection.ExecuteAsync(query, secao);
        }

        public async Task Update(Secao secao)
        {
            using var connection = _dbConnection;
            var query = "UPDATE Secao SET Descricao = @Descricao, FilialId = @FilialId WHERE Id = @Id";
            await connection.ExecuteAsync(query, secao);
        }

        public async Task Delete(int id)
        {
            using var connection = _dbConnection;
            var query = "DELETE FROM Secao WHERE Id = @Id";
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}