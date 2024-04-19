﻿using Microsoft.Extensions.DependencyInjection;

namespace Facturacion.Infrastructure.Extensions;

public static class InjectionExtensions
{
    public static IServiceCollection AddInjectionInfraestructure(this IServiceCollection services)
    {
        services.AddSingleton<FacturacionDbContext>();
        services.AddScoped<IActividadesRepository, ActividadesRepository>();
        return services;
    }
}


//         services.AddSingleton<ApplicationDbContext>();
//         services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//         services.AddTransient<IUnitOfWork, UnitOfWork>();
//         services.AddScoped<IProductRepository, ProductRepository>();
//         return services;