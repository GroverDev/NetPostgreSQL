using AutoMapper;
using Facturacion.Domain;
using Siat.Sincronizacion;

namespace Facturacion.Application.Mappers;

public class ActividadMappingProfile:Profile
{
    public ActividadMappingProfile()
    {
        CreateMap<Actividades, actividadesDto>()
        .ReverseMap();
    }
}

// using AutoMapper;
// using Store.Application.Dtos.Response;
// using Store.Domain;

// public class ProductMappingProfile : Profile
// {
//     public ProductMappingProfile()
//     {
//         CreateMap<Product, ProductResponseDto>()
//         .ForMember(x =>x.ProviderName, x => x.MapFrom(y=> "sadas"))
//         .ReverseMap();
//     }
// }
