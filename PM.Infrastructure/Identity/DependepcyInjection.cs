using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PM.Application.Common.Interfaces.ISercices;
using PM.Domain.Entities;
using PM.Infrastructure.Identity.Services;
using PM.Infrastructure.Persistence;

namespace PM.Infrastructure.Identity;

public static class DependepcyInjection
{
    public static IServiceCollection AddIdentity(
        this IServiceCollection services)
    {
        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IIdentityService, IdentityService>();

        return services;
    }
}
