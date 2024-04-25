using Common.Utilities;

namespace Facturacion.Application;

public interface IProductosServiciosApplication
{
    public Task<Response<bool>> UpdateProductos(int createdBy);

}
