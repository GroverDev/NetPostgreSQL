using AutoMapper;
using Store.Application.Dtos.Response;
using Store.Domain;

namespace Store.Application.Mappers;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductResponseDto>()
        .ForMember(x =>x.ProviderName, x => x.MapFrom(y=> "sadas"))
        .ReverseMap();
    }
}
