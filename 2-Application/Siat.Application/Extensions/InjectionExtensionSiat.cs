
using System.Reflection;
//using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Siat.Application.Extensions;

public static class InjectionExtensionSiat
{
     public static IServiceCollection AddInjectionSiat(this IServiceCollection services)
    {
        //services.AddSingleton(configuration);
        // services.AddFluentValidation(options => {
        //     options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(p=>p.IsDynamic));
        // });

        //services.AddAutoMapper(Assembly.GetExecutingAssembly());
        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<ISincronizacionApplication, SincronizacionApplication>();
        services.AddScoped<ICodigosApplication, CodigosApplication>();
        services.AddScoped<ICompraVentaApplication,CompraVentaApplication>();
        return services;
    }
}
