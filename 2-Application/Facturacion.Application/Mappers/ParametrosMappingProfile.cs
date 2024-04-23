using AutoMapper;
using Facturacion.Domain;
using Siat.Sincronizacion;

namespace Facturacion.Application;

public class ParametrosMappingProfile : Profile
{
    public ParametrosMappingProfile()
    {
        CreateMap<Parametros, parametricasDto>()
        .ReverseMap();
    }
}
