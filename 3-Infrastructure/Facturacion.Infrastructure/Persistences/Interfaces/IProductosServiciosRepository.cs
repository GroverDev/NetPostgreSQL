using Facturacion.Domain;

namespace Facturacion.Infrastructure;

public interface IProductosServiciosRepository
{
    Task<bool> CreateProductoServicio(ProductosServicios producto);   

    Task<ProductosServicios> GetProductoServicioByCodigo(string CodigoActividad, long CodigoProducto);

    Task<bool> DisableAllProductosServicios();

    Task<bool> EnableProductoServicio(ProductosServicios producto);  
}
