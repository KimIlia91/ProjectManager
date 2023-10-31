using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PM.Application.Common.Identity.Models;
using PM.Infrastructure.Persistence;

namespace PM.Infrastructure.Identity;

internal static class DependepcyInjection
{
    public static IServiceCollection AddIdentityConfig(
        this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
