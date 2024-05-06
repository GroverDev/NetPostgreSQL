using Facturacion.Domain;

namespace Facturacion.Infrastructure;

public interface IPuntoVentaRepository
{
    Task<PuntoVenta> GetPuntoVentaByCodigo(int Codigo);

}
