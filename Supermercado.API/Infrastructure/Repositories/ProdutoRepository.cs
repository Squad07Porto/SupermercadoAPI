using System.Data;
using Dapper;
using Supermercado.API.Infrastructure.Repositories.Interfaces;
using Supermercado.API.Models;

namespace Supermercado.API.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProdutoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            using var connection = _dbConnection;
            var query = "SELECT * FROM Produto";
            return await connection.QueryAsync<Produto>(query);
        }

        public async Task<Produto?> GetById(int id)
        {
            using var connection = _dbConnection;
            var query = "SELECT * FROM Produto WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Produto>(query, new { Id = id });
        }

        public async Task Add(Produto produto)
        {
            using var connection = _dbConnection;
            var query = "INSERT INTO Produto (Codigo, Nome, Preco, Quantidade, SecaoId)" + 
                        "VALUES (@Codigo, @Nome, @Preco, @Quantidade, @SecaoId)";
            await connection.ExecuteAsync(query, produto);
        }

        public async Task Update(Produto produto)
        {
            using var connection = _dbConnection;
            var query = "UPDATE Produto SET" +
                        "Codigo = @Codigo, " +
                        "Nome = @Nome, " +
                        "Preco = @Preco, " +
                        "Quantidade = @Quantidade" +
                        "WHERE Id = @Id";
            await connection.ExecuteAsync(query, produto);
        }

        public async Task Delete(int id)
        {
            using var connection = _dbConnection;
            var query = "DELETE FROM Produto WHERE Id = @Id";
            await connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<IEnumerable<Produto>> GetBySecaoId(int secaoId)
        {
            using var connection = _dbConnection;
            var query = "SELECT * FROM Produto WHERE SecaoId = @SecaoId";
            return await connection.QueryAsync<Produto>(query, new { SecaoId = secaoId });
        }

        public async Task<IEnumerable<Produto>> GetByFilialId(int filialId)
        {
            using var connection = _dbConnection;
            var query = @"
                            SELECT *
                            FROM Produto
                            JOIN Secao ON Produto.SecaoId = Secao.Id
                            WHERE Secao.FilialId = @FilialId
                        ";
            return await connection.QueryAsync<Produto>(query, new { FilialId = filialId });
        }
    }
}
