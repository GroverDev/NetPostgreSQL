using AutoMapper;
using Facturacion.Domain;
using Siat.Codigos;
namespace Facturacion.Application;

public class CufdMappingProfile: Profile
{
    public CufdMappingProfile()
    {
        CreateMap<Cufd, respuestaCufd>()
        .ReverseMap();
    }
}
