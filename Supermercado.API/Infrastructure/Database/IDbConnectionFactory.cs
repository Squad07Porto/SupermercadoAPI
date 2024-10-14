using System.Data;

namespace Supermercado.API.Infrastructure.Database
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}