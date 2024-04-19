using Common.Utilities;

namespace Facturacion.Application;

public interface IActividadesDocumentoSectorApplication
{
    public Task<Response<bool>> UpdateActividadesDocumentoSector(int createdBy);
}
