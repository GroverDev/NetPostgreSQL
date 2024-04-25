using Common.Utilities;
using Siat.CompraVenta;

namespace Siat.Application;

public interface ICompraVentaApplication
{
    public Task<Response<bool>> OkComunnication();
    public Task<Response<respuestaRecepcion>> GetRecepcionFactura(int codigoPuntoVenta, int codigoSucursal, string cufd, string cuis,
        byte[] archivo, int codigoDocumentoSector,int codigoEmision, 
        string fechaEnvio, string hashArchivo, int tipoFacturaDocumento);
    public Task<Response<respuestaRecepcion>> GetAnulacionFactura(int codigoPuntoVenta, int codigoSucursal, string cufd, string cuis,
      string cuf, int codigoDocumentoSector,int codigoEmision, 
      string fechaEnvio, string hashArchivo, int tipoFacturaDocumento);
}
