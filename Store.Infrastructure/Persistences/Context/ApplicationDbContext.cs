﻿using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Store.Infrastructure.Persistences;

public class ApplicationDbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("StoreConnection")!;
    }

    public IDbConnection CreateConnection => new NpgsqlConnection(_connectionString);
}
