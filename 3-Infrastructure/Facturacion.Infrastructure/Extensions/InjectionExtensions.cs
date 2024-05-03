using Microsoft.Extensions.DependencyInjection;

namespace Facturacion.Infrastructure.Extensions;

public static class InjectionExtensions
{
    public static IServiceCollection AddInjectionInfraestructure(this IServiceCollection services)
    {
        services.AddSingleton<FacturacionDbContext>();
        services.AddScoped<IActividadesRepository, ActividadesRepository>();
        services.AddScoped<IActividadesDocumentoSectorRepository, ActividadesDocumentoSectorRepository>();
        services.AddScoped<ILeyendasFacturaRepository, LeyendasFacturaRepository>();
        services.AddScoped<IParametrosRepository, ParametrosRepository>();
        services.AddScoped<IProductosServiciosRepository, ProductosServiciosRepository>();
        services.AddScoped<ISincronizacionRequestRepository, SincronizacionRequestRepository>();
        services.AddScoped<ICufdRepository, CufdRepository>();
        return services;
    }
}


//         services.AddSingleton<ApplicationDbContext>();
//         services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//         services.AddTransient<IUnitOfWork, UnitOfWork>();
//         services.AddScoped<IProductRepository, ProductRepository>();
//         return services;
