using Microsoft.Extensions.DependencyInjection;
using PM.Application.Common.Interfaces.ISercices;

namespace PM.Infrastructure.Services;

/// <summary>
/// Dependency injection extension for registering application services.
/// </summary>
public static class ServicesDi
{
    /// <summary>
    /// Adds application-specific services to the provided service collection.
    /// </summary>
    /// <param name="services">The service collection to which services will be added.</param>
    /// <returns>The modified service collection with added services.</returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<IDateTimeService, DateTimeService>();

        return services;
    }
}
