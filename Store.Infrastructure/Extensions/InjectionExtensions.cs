using Microsoft.Extensions.DependencyInjection;
using Store.Infrastructure.Persistences;

namespace Store.Infrastructure;

public static class InjectionExtensions
{
    public static IServiceCollection AddInjectionInfraestructure(this IServiceCollection services)
    {
        services.AddSingleton<ApplicationDbContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }
}
