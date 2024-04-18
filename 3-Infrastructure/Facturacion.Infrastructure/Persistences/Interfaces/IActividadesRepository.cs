using Facturacion.Domain;

namespace Facturacion.Infrastructure;

public interface IActividadesRepository
{
    Task<bool> DeleteAllActividades();

    Task<bool> CreateActividad(Actividad actividad);   

    Task<Actividad> GetActividadByCodigo(string CodigoCaeb);
}  
