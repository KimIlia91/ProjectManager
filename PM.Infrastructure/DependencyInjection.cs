using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PM.Infrastructure.Persistence;
using PM.Infrastructure.Services;

namespace PM.Infrastructure;

public static class DependencyInjection
{
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
