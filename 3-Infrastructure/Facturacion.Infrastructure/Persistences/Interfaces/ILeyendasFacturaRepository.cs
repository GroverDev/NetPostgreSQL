using Facturacion.Domain;

namespace Facturacion.Infrastructure;

public interface ILeyendasFacturaRepository
{
    Task<bool> CreateLeyendaFactura(LeyendasFactura leyenda);   

    Task<LeyendasFactura> GetLeyendaFacturaByCodigo(string codigoActividad, string descripcionLeyenda);

    Task<bool> DisableAllLeyendasFactura();

    Task<bool> EnableLeyendaFactura(LeyendasFactura actividad);   
}
