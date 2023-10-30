using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PM.Infrastructure.Persistence;

namespace PM.Infrastructure;

public static class DependepcyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddPesistence();

        return services;
    }
}
