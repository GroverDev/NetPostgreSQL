using Common.Utilities;

namespace Facturacion.Application;

public interface ICufdApplication
{
    public Task<Response<bool>> UpdateCufd(int createdBy);
}
