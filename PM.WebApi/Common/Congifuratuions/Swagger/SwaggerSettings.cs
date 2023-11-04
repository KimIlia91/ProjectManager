using System.Reflection;
using Microsoft.OpenApi.Models;
using PM.WebApi.Common.Congifuratuions.Constants;

namespace PM.WebApi.Common.Congifuratuions.Swagger;

/// <summary>
/// Static class containing settings and extension methods for configuring Swagger documentation.
/// </summary>
public static class SwaggerSettings
{
    /// <summary>
    /// Adds custom Swagger generation settings to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="executingAssembly">The assembly from which the Swagger settings are executed.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddCustomSwaggerGen(
        this IServiceCollection services,
        Assembly executingAssembly)
    {
        services.AddRouting(options => options.LowercaseUrls = true);

        return services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(DomainApiConstants.Version,
                new OpenApiInfo { Title = DomainApiConstants.TitleApi, Version = DomainApiConstants.Version });
            c.DescribeAllParametersInCamelCase();
            c.OperationFilter<AcceptLanguageHeaderParameter>();
            c.AddSecurityDefinition(DomainApiConstants.AuthScheme, new OpenApiSecurityScheme
            {
                Description = DomainApiConstants.AuthDescription,
                Name = DomainApiConstants.AuthNameHeader,
                Type = SecuritySchemeType.ApiKey,
                Scheme = DomainApiConstants.AuthScheme,
                In = ParameterLocation.Header
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = DomainApiConstants.AuthScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });

            var xmlFile = $"{executingAssembly.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath, true);

            var referencedAssembliesNames = executingAssembly.GetReferencedAssemblies().Distinct();
            foreach (var assemblyName in referencedAssembliesNames)
            {
                var relatedXmlFile = $"{assemblyName.Name}.xml";
                var relatedXmlPath = Path.Combine(AppContext.BaseDirectory, relatedXmlFile);
                if (File.Exists(relatedXmlPath))
                {
                    c.IncludeXmlComments(relatedXmlPath, true);
                }
            }
        });
    }

    /// <summary>
    /// Configures and adds Swagger middleware to the specified web application.
    /// </summary>
    /// <param name="app">The web application to configure for Swagger.</param>
    /// <returns>The configured web application.</returns>
    public static WebApplication UseCustomSwaggerConfiguration(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project Manager v1");
        });

        return app;
    }
}
