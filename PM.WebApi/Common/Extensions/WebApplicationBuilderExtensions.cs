using Microsoft.AspNetCore.Mvc.Infrastructure;
using PM.Application;
using PM.Infrastructure;
using PM.WebApi.Common.Congifuratuions.Swagger;
using PM.WebApi.Common.Errors;
using System.Reflection;

namespace PM.WebApi.Common.Extensions;

/// <summary>
/// A static class containing extension methods for configuring a WebApplicationBuilder.
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Configures services for a WebApplicationBuilder.
    /// </summary>
    /// <param name="builder">The WebApplicationBuilder instance to configure services for.</param>
    /// <returns>The configured WebApplicationBuilder instance.</returns>
    public static WebApplicationBuilder ConfigureServices(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddControllers();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddSingleton<ProblemDetailsFactory, PmErrorProblemDitailsFactory>();
        builder.Services.AddApplication();
        builder.Services.AddCustomSwaggerGen(Assembly.GetExecutingAssembly());

        return builder;
    }
}
