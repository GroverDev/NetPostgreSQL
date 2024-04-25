using Facturacion.Domain;

namespace Facturacion.Infrastructure;

public class SincronizacionRequestRepository : ISincronizacionRequestRepository
{
    public async Task<SincronizacionRequest> GetSincronizacionRequest(int idPuntoVenta)
    {
        var sincronizacionRequest = new SincronizacionRequest
        {
            CodigoCUIS = "E8465CDD",
            CodigoPuntoVenta = 0,
            CodigoSucursal = 5
        };

        return sincronizacionRequest;
    }
}
