using AutoMapper;
using Facturacion.Domain;
using Siat.Sincronizacion;

namespace Facturacion.Application;

public class ProductosServiciosMappingProfile:Profile
{
    public ProductosServiciosMappingProfile()   
    {
        CreateMap<ProductosServicios, productosDto>()
        .ReverseMap();
    }
}
