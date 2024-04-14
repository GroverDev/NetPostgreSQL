using Common.Utilities;

namespace Facturacion.Application;

public interface ISiatSincronizacionApplication
{
    Task<Response<bool>> OkComunnication();
}

