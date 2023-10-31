using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PM.Infrastructure.Identity;
using PM.Infrastructure.Persistence;
using PM.Infrastructure.Services;

namespace PM.Infrastructure;

public static class DependepcyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddIdentityConfig();
        services.AddPesistence();
        services.AddServices();
        return services;
    }
}
