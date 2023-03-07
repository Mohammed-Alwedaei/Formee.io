using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Links.DataAccess.Contexts;

public class DbContext
{
    private readonly IConfiguration _configuration;

    public DbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection Connect()
    {
        var connectionString = _configuration
            .GetConnectionString("DefaultConnection");

        var connection = new SqlConnection(connectionString);

        return connection;
    }
}
