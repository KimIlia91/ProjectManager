using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PM.Infrastructure.Auth;
using PM.Infrastructure.Persistence;
using PM.Infrastructure.Services;

namespace PM.Infrastructure;

/// <summary>
/// Dependency injection extension for configuring infrastructure services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Configures and adds infrastructure-related services to the provided service collection.
    /// </summary>
    /// <param name="services">The service collection to which services will be added.</param>
    /// <param name="configuration">The application's configuration settings.</param>
    /// <returns>The modified service collection with added infrastructure services.</returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddPesistence(configuration);
        services.AddServices();
        services.AddAuth(configuration);

        return services;
    }
}
