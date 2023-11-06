using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PM.Application.Common.Behaviors;
using PM.Application.Common.Mapping;
using System.Reflection;

namespace PM.Application;

/// <summary>
/// 
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds application-specific services to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which services should be added.</param>
    /// <returns>The <see cref="IServiceCollection"/> with added application services.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMapping(assembly);
        services.AddValidatorsFromAssembly(assembly);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        return services;
    }
}
