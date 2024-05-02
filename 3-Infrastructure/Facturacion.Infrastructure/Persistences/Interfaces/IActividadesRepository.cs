using Facturacion.Domain;

namespace Facturacion.Infrastructure;

public interface IActividadesRepository
{
    

    Task<bool> CreateActividad(Actividades actividad);   

    Task<Actividades> GetActividadByCodigo(string CodigoActividad);
    Task<bool> DisableAllActividades();
    Task<bool> EnableActividad(Actividades actividad);
}  
