using Microsoft.Extensions.DependencyInjection;
using PM.Application.Common.Interfaces.ISercices;

namespace PM.Infrastructure.Services;

public static class ServicesDi
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IDateTimeService, DateTimeService>();

        return services;
    }
}
