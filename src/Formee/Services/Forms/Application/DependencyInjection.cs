using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

/// <summary>
/// Configure the dependency injection of the application layer
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Register all application layer services to the dependency injection container
    /// </summary>
    /// <param name="services"></param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(cfg 
            => cfg.RegisterServicesFromAssemblies(
                Assembly.GetExecutingAssembly()));
        return services;
    }
}
