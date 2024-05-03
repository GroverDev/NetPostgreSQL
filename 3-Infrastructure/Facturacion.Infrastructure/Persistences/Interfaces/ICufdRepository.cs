using Facturacion.Domain;

namespace Facturacion.Infrastructure;

public interface ICufdRepository
{
    Task<bool> CreateCufd(Cufd leyenda);

}
