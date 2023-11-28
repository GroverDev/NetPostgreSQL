using Dapper;
using Store.Infrastructure.Interfaces;
using Store.Infrastructure.Persistences;

namespace Store.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<T>> GetAllAsync(string customQuery)
    {
        using var connection = _context.CreateConnection;
        return await connection.QueryAsync<T>(customQuery);
    }

    public async Task<T> GetByIdAsync(string customQuery, object parameter)
    {
        using var connection = _context.CreateConnection;
        var objParam = new DynamicParameters(parameter);
        return await connection.QuerySingleOrDefaultAsync<T>(customQuery, param: objParam);
    }

    public async Task<bool> ExecAsync(string customQuery, object parameters)
    {
         using var connection = _context.CreateConnection;
         var objParam = new DynamicParameters(parameters);
         var recordsAffected = await connection.ExecuteAsync(customQuery, objParam);
         return recordsAffected >0;
    }
}
