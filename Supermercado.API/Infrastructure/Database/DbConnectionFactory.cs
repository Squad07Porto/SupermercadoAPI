using System.Data;
using MySqlConnector;

namespace Supermercado.API.Infrastructure.Database
{
    public class DbConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration = configuration;

        public IDbConnection CreateConnection()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new MySqlConnection(connectionString);
        }
    }
}