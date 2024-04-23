using Common.Utilities;

namespace Facturacion.Application;

public interface IParametrosApplication
{
    public Task<Response<bool>> UpdateParametros(int createdBy);

}
