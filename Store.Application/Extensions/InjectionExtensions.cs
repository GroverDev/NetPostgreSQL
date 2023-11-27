using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Store.Application.Interfaces;
using Store.Application.Services;

namespace Store.Application.Extensions;

public static class InjectionExtensions
{
    public static IServiceCollection AddInjectionApplication(this IServiceCollection services)
    {
        //services.AddSingleton(configuration);
        services.AddFluentValidation(options => {
            options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(p=>p.IsDynamic));
        });

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IProductApplication, ProductApplication>();
        return services;
    }
}
