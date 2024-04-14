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

        //services.AddAutoMapper(Assembly.GetExecutingAssembly());
        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<ISincronizacionApplication, SincronizacionApplication>();
        return services;
    }
}


