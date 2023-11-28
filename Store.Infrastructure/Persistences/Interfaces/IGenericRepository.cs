namespace Store.Infrastructure.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(string customQuery);
    Task<T> GetByIdAsync(string customQuery, object parameter);
    Task<bool> ExecAsync(string customQuery, object parameters);
}
