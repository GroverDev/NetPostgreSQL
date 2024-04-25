using System.Reflection;
//using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Facturacion.Application.Extensions;

public static class InjectionExtensionsFacturacion
{
     public static IServiceCollection AddInjectionApplication(this IServiceCollection services)
    {
        //services.AddSingleton(configuration);
        // services.AddFluentValidation(options => {
        //     options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(p=>p.IsDynamic));
        // });

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<IActividadesApplication, ActividadesApplication>();
        services.AddScoped<IActividadesDocumentoSectorApplication, ActividadesDocumentoSectorApplication>();
        services.AddScoped<ILeyendasFacturaApplication, LeyendasFacturaApplication>();
        services.AddScoped<IParametrosApplication, ParametrosApplication>();
        services.AddScoped<IProductosServiciosApplication, ProductosServiciosApplication>();
        return services;
    }
}


