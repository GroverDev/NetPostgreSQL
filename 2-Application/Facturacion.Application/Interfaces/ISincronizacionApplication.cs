using Common.Utilities;

namespace Facturacion.Application;

public interface ISincronizacionApplication
{
    Task<Response<bool>> OkComunnication();
}

