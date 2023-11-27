using Store.Domain;

namespace Store.Infrastructure;

public interface IProductRepository
{
    Task<IEnumerable<Product>> ListProducts();
}
