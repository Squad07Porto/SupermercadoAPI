using System.Data;
using Dapper;
using Supermercado.API.Infrastructure.Repositories.Interfaces;
using Supermercado.API.Models;

namespace Supermercado.API.Infrastructure.Repositories
{
    public class FuncionarioRepository(IDbConnection dbConnection) : IFuncionarioRepository
    {
        private readonly IDbConnection _dbConnection = dbConnection;

        public async Task<IEnumerable<Funcionario>> GetAll()
        {
            using var connection = _dbConnection;
            var query = "SELECT * FROM Funcionario";
            return await connection.QueryAsync<Funcionario>(query);
        }

        public async Task<Funcionario?> GetById(int id)
        {
            using var connection = _dbConnection;
            var query = "SELECT * FROM Funcionario WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Funcionario>(query, new { Id = id });
        }

        public async Task Add(Funcionario funcionario)
        {
            using var connection = _dbConnection;
            var query = "INSERT INTO Funcionario (Nome, Cargo, Cpf) VALUES (@Nome, @Cargo, @Cpf)";
            await connection.ExecuteAsync(query, funcionario);
        }

        public async Task Update(Funcionario funcionario)
        {
            using var connection = _dbConnection;
            var query = "UPDATE Funcionario SET Nome = @Nome, Cargo = @Cargo, Cpf = @Cpf WHERE Id = @Id";
            await connection.ExecuteAsync(query, funcionario);
        }

        public async Task Delete(int id)
        {
            using var connection = _dbConnection;
            var query = "DELETE FROM Funcionario WHERE Id = @Id";
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}