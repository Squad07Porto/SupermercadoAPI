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
      var query = "INSERT INTO Produto (Nome, Preco, Quantidade) VALUES (@Nome, @Preco, @Quantidade)";
      await connection.ExecuteAsync(query, produto);
    }

    public async Task Update(Produto produto)
    {
      using var connection = _dbConnection;
      var query = "UPDATE Produto SET Nome = @Nome, Preco = @Preco, Quantidade = @Quantidade WHERE Id = @Id";
      await connection.ExecuteAsync(query, produto);
    }

    public async Task Delete(int id)
    {
      using var connection = _dbConnection;
      var query = "DELETE FROM Produto WHERE Id = @Id";
      await connection.ExecuteAsync(query, new { Id = id });
    }
  }
}
