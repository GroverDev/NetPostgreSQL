using Facturacion.Domain;

namespace Facturacion.Infrastructure;

public interface IFacturasRepository
{
    Task<bool> CreateFactura(Factura factura);
}
