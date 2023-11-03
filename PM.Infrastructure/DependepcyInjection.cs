using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PM.Application.Common.Interfaces.ISercices;
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
        services.AddPesistence(configuration);
        services.AddIdentity();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}
