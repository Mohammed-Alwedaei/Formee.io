using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Links.BusinessLogic.Contexts;

public class AppContextService
{
    private readonly IConfiguration _configuration;

    public AppContextService(IConfiguration configuration)
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
