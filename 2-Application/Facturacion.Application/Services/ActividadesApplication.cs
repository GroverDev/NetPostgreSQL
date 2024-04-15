using Common.Utilities;
using Siat.Application;

namespace Facturacion.Application;

public class ActividadesApplication: IActividadesApplication
{
    private readonly ISincronizacionApplication _sincronizacionApplication;

    public ActividadesApplication(ISincronizacionApplication sincronizacionApplication)
    {
        _sincronizacionApplication = sincronizacionApplication;
    }

    public async Task<Response<bool>> OkComunnication()
    {
        return await _sincronizacionApplication.OkComunnication();
    }

    public Task<Response<bool>> UpdateActividades()
    {
        throw new NotImplementedException();
    }
}
