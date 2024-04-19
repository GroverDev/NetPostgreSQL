using Facturacion.Domain;

namespace Facturacion.Infrastructure;
public interface IActividadesDocumentoSectorRepository
{
    Task<bool> CreateActividadDocumentoSector(ActividadesDocumentoSector actividad);   

    Task<ActividadesDocumentoSector> GetActividadDocumentoSectorByCodigo(int codigoDocumentoSector, string codigoActividad);

    Task<bool> DisableAllActividadDocumentoSector();

    Task<bool> EnableActividadDocumentoSector(ActividadesDocumentoSector actividad);   
}





