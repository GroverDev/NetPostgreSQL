using Common.Utilities;

namespace Facturacion.Application;

public interface IActividadesApplication
{
    public Task<Response<bool>> UpdateActividades();
    public Task<Response<bool>> OkComunnication();
}
