using Microsoft.Extensions.DependencyInjection;
using Store.Infrastructure.Interfaces;
using Store.Infrastructure.Persistences;
using Store.Infrastructure.Repositories;

namespace Store.Infrastructure;

public static class InjectionExtensions
{
    public static IServiceCollection AddInjectionInfraestructure(this IServiceCollection services)
    {
        services.AddSingleton<ApplicationDbContext>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }
}
