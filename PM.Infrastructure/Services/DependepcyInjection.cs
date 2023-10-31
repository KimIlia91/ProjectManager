using Microsoft.Extensions.DependencyInjection;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Infrastructure.Persistence;
using PM.Infrastructure.Persistence.Repositories;

namespace PM.Infrastructure.Services;

public static class DependepcyInjection
{
    public static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        services.AddScoped<IApplicationUserService, ApplicationUserService>();

        return services;
    }
}
