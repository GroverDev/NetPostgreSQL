using Common.Utilities;

namespace Facturacion.Application;

public interface ILeyendasFacturaApplication
{
    public Task<Response<bool>> UpdateLeyendasFactura(int createdBy);

    Task<string> GetLeyendaFacturaAleatoria(string CodigoActividad);

    
}
