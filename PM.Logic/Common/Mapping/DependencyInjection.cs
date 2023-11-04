using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PM.Application.Common.Mapping;

/// <summary>
/// A static class providing extension methods for configuring dependency injection in the application.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds mapping services to the <see cref="IServiceCollection"/> based on the specified <paramref name="assembly"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which services should be added.</param>
    /// <param name="assembly">The assembly to scan for mapping configurations.</param>
    /// <returns>The <see cref="IServiceCollection"/> with added mapping services.</returns>
    public static IServiceCollection AddMapping(
        this IServiceCollection services,
        Assembly assembly)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(assembly);
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
}
