using Common.Utilities;
using Siat.Sincronizacion;

namespace Siat.Application;

public interface ISincronizacionApplication
{
    public Task<Response<bool>> OkComunnication();
    public Task<Response<List<actividadesDto>>> GetActividades(int codigoPuntoVenta, int codigoSucursal, string cuis);
    public Task<Response<string>> GetFechaYHora(int codigoPuntoVenta, int codigoSucursal, string cuis);
    public Task<Response<List<actividadesDocumentoSectorDto>>> GetActividadesDocumentoSector(int codigoPuntoVenta, int codigoSucursal, string cuis);
    public Task<Response<List<parametricaLeyendasDto>>> GetParametricasLeyendasFactura(int codigoPuntoVenta, int codigoSucursal, string cuis);
    public Task<Response<List<parametricasDto>>> GetParametricasMensajesServicios(int codigoPuntoVenta, int codigoSucursal, string cuis);
    public Task<Response<List<productosDto>>> GetProductosServicios(int codigoPuntoVenta, int codigoSucursal, string cuis);
    public Task<Response<List<parametricasDto>>> GetParametricasEventosSignificativos(int codigoPuntoVenta, int codigoSucursal, string cuis);
    public Task<Response<List<parametricasDto>>> GetParametricasMotivoAnulacion(int codigoPuntoVenta, int codigoSucursal, string cuis);
    public Task<Response<List<parametricasDto>>> GetParametricasPaisOrigen(int codigoPuntoVenta, int codigoSucursal, string cuis);
    public Task<Response<List<parametricasDto>>> GetParametricasTipoDocumentoIdentidad(int codigoPuntoVenta, int codigoSucursal, string cuis);
    public Task<Response<List<parametricasDto>>> GetParametricasTipoDocumentoSector(int codigoPuntoVenta, int codigoSucursal, string cuis);
    public Task<Response<List<parametricasDto>>> GetParametricasTipoEmision(int codigoPuntoVenta, int codigoSucursal, string cuis);
    public Task<Response<List<parametricasDto>>> GetParametricasTipoHabitacion(int codigoPuntoVenta, int codigoSucursal, string cuis);
    public Task<Response<List<parametricasDto>>> GetParametricasTipoMetodoPago(int codigoPuntoVenta, int codigoSucursal, string cuis);
    public Task<Response<List<parametricasDto>>> GetParametricasTipoMoneda(int codigoPuntoVenta, int codigoSucursal, string cuis);
    public Task<Response<List<parametricasDto>>> GetParametricasTipoPuntoVenta(int codigoPuntoVenta, int codigoSucursal, string cuis);
    public Task<Response<List<parametricasDto>>> GetParametricasTiposFactura(int codigoPuntoVenta, int codigoSucursal, string cuis);
    public Task<Response<List<parametricasDto>>> GetParametricasUnidadMedida(int codigoPuntoVenta, int codigoSucursal, string cuis);
}
