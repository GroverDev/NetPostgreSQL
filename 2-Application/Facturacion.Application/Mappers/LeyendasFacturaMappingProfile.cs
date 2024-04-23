using AutoMapper;
using Facturacion.Domain;
using Siat.Sincronizacion;

namespace Facturacion.Application;

public class LeyendasFacturaMappingProfile: Profile
{
    public LeyendasFacturaMappingProfile()
    {
        CreateMap<LeyendasFactura, parametricaLeyendasDto>()
        .ReverseMap();
    }
}
