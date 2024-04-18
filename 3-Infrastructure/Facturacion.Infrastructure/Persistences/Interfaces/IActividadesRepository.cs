using Facturacion.Domain;

namespace Facturacion.Infrastructure;

public interface IActividadesRepository
{
    Task<bool> DeleteAllActividades();

    Task<bool> CreateActividad(Actividades actividad);   

    Task<Actividades> GetActividadByCodigo(string CodigoCaeb);
}  
