using System.Data;
using Dapper;
using Supermercado.API.Infrastructure.Repositories.Interfaces;
using Supermercado.API.Models;

namespace Supermercado.API.Infrastructure.Repositories
{
    public class UsuarioRepository(IDbConnection dbConnection) : IUsuarioRepository
    {
        private readonly IDbConnection _dbConnection = dbConnection;

        public async Task<int> CreateUserAsync(Usuario usuario)
        {
            using var connection = _dbConnection;
            var query = @"INSERT INTO Usuario (Email, Senha, Salt, Cargo) VALUES (@Email, @Senha, @Salt, @Cargo)";
            return await connection.ExecuteAsync(query, usuario);
        }

        public async Task<Usuario?> GetUserByEmailAsync(string email)
        {
            using var connection = _dbConnection;
            var query = "SELECT * FROM Usuario WHERE Email = @Email";
            return await connection.QueryFirstOrDefaultAsync<Usuario>(query, new { Email = email });
        }
    }
}