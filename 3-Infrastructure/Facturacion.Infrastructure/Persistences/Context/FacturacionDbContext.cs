using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Facturacion.Infrastructure;


public class FacturacionDbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public FacturacionDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("FacturacionConnection")!;
    }

    public IDbConnection CreateConnection => new NpgsqlConnection(_connectionString);
}