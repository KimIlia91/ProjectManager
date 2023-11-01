using Microsoft.Extensions.DependencyInjection;
using PM.Application.Common.Interfaces.ISercices;

namespace PM.Infrastructure.Services;

public static class DependepcyInjection
{
    public static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        services.AddScoped<IIdentityService, IdentityService>();

        return services;
    }
}
