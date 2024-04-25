using Facturacion.Domain;

namespace Facturacion.Infrastructure;

public interface ISincronizacionRequestRepository
{
    Task<SincronizacionRequest> GetSincronizacionRequest(int idPuntoVenta);
}
