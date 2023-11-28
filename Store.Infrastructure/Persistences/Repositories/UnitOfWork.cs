using Store.Domain;
using Store.Infrastructure.Interfaces;

namespace Store.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public IGenericRepository<Product> Product { get; }
    public UnitOfWork(IGenericRepository<Product> product)
    {
        Product = product;
    }
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
