using Microsoft.Extensions.DependencyInjection;

namespace Store.Infrastructure;

public static class InjectionExtensions
{
    public static IServiceCollection AddInjectionPersistence(this IServiceCollection services)
    {
        services.AddSingleton<ApplicationDbContext>();
        return services;
    }
}
