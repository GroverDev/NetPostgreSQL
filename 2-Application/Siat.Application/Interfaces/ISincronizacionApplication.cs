using Common.Utilities;
using Siat.Sincronizacion;

namespace Siat.Application;

public interface ISincronizacionApplication
{
    public Task<Response<bool>> OkComunnication();

    public Task<Response<List<actividadesDto>>> GetActividades(int codigoPuntoVenta, int codigoSucursal, string cuis);
}
