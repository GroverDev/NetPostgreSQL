using AutoMapper;
using Facturacion.Domain;
using Siat.Sincronizacion;

namespace Facturacion.Application.Mappers;

public class ActividadesDocumentoSectorMappingProfile: Profile
{
    public ActividadesDocumentoSectorMappingProfile()
    {
        CreateMap<ActividadesDocumentoSector, actividadesDocumentoSectorDto>()
        .ReverseMap();
    }
}
