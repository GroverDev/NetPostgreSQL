using Store.Domain;

namespace Store.Infrastructure.Interfaces;

public interface IUnitOfWork: IDisposable
{
    IGenericRepository<Product> Product {get;}

}
