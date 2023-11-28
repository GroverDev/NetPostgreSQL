using Dapper;
using Store.Domain;
using Store.Infrastructure.Persistences;

namespace Store.Infrastructure;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        using var connection = _context.CreateConnection;
        var query = @"SELECT p.id, 
                              p.product_code, 
                              p.product_name, 
                              p.description, 
                              p.sale_price, 
                              p.provider_id, 
                              p.initial_stock, 
                              pd.provider_name , 
                              p.is_active,
                              p.current_stock,
                              p.min_reorder_quantity
                        FROM products p
                             INNER JOIN providers pd
                          ON pd.id = p.provider_id 
                        WHERE p.state ";
        var products = await connection.QueryAsync<Product>(query);
        return products;
    }

    // public async Task<IEnumerable<Product>> ListProducts()
    // {
    //     using var connection = _context.CreateConnection;
    //     var query = @"SELECT p.id, 
    //                           p.product_code, 
    //                           p.product_name, 
    //                           p.description, 
    //                           p.sale_price, 
    //                           p.provider_id, 
    //                           p.initial_stock, 
    //                           pd.provider_name , 
    //                           p.is_active,
    //                           p.current_stock,
    //                           p.min_reorder_quantity
    //                     FROM products p
    //                          INNER JOIN providers pd
    //                       ON pd.id = p.provider_id 
    //                     WHERE p.state ";
    //     var products = await connection.QueryAsync<Product>(query);
    //     return products;
    // }
}
