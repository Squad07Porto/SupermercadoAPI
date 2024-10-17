using System.Data;
using Dapper;
using Supermercado.API.Infrastructure.Repositories.Interfaces;
using Supermercado.API.Models;

namespace Supermercado.API.Infrastructure.Repositories
{
    public class EquipamentoRepository(IDbConnection dbConnection) : IEquipamentoRepository
    {
        private readonly IDbConnection _dbConnection = dbConnection;

        public async Task<IEnumerable<Equipamento>> GetAll()
        {
            using var connection = _dbConnection;
            var query = "SELECT * FROM Equipamento";
            return await connection.QueryAsync<Equipamento>(query);
        }

        public async Task<Equipamento?> GetById(int id)
        {
            using var connection = _dbConnection;
            var query = "SELECT * FROM Equipamento WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Equipamento>(query, new { Id = id });
        }

        public async Task Add(Equipamento equipamento)
        {
            using var connection = _dbConnection;
            var query = "INSERT INTO Equipamento (TipoEquipamento, Descricao) VALUES (@TipoEquipamento, @Descricao)";
            await connection.ExecuteAsync(query, equipamento);
        }

        public async Task Update(Equipamento equipamento)
        {
            using var connection = _dbConnection;
            var query = "UPDATE Equipamento SET TipoEquipamento = @TipoEquipamento, Descricao = @Descricao WHERE Id = @Id";
            await connection.ExecuteAsync(query, equipamento);
        }

        public async Task Delete(int id)
        {
            using var connection = _dbConnection;
            var query = "DELETE FROM Equipamento WHERE Id = @Id";
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }


}