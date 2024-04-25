using Common.Utilities;
using Siat.Codigos;

namespace Siat.Application;

public interface ICodigosApplication
{
    public Task<Response<bool>> OkComunnication();
    public Task<Response<respuestaCuis>> GetCUIS(int codigoPuntoVenta, int codigoSucursal);
    public Task<Response<respuestaCufd>> GetCUFD(int codigoPuntoVenta, int codigoSucursal, string cuis);
    public Task<Response<respuestaVerificarNit>> GetVerificarNit(int codigoPuntoVenta, int codigoSucursal, string cuis);
    public Task<Response<respuestaNotificaRevocado>> GetNotificaRevocacion(string certificado, int codigoSucursal, string cuis, DateTime fechaRevocacion, string razonRevocacion);
    
}
