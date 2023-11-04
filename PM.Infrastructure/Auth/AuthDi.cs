using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PM.Application.Common.Interfaces.ISercices;
using PM.Domain.Entities;
using PM.Infrastructure.Auth.Services;
using PM.Infrastructure.Identity;
using PM.Infrastructure.Identity.Services;
using PM.Infrastructure.Identity.Settings;
using PM.Infrastructure.Persistence;
using System.Text;

namespace PM.Infrastructure;

/// <summary>
/// A static class for configuring authentication services.
/// </summary>
public static class AuthDi
{
    /// <summary>
    /// Configures and adds authentication services to the specified service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> instance.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddAuth(
        this IServiceCollection services,
         IConfiguration configuration)
    {
        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        var jwtSettings = new JwtSettings();
        var tokenValidationSettings = new TokenValidationSettings();
        configuration.Bind(nameof(JwtSettings), jwtSettings);
        configuration.Bind(nameof(TokenValidationSettings), tokenValidationSettings);
        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton(Options.Create(tokenValidationSettings));

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = tokenValidationSettings.ValidateIssuer,
                    ValidateAudience = tokenValidationSettings.ValidateAudience,
                    ValidateLifetime = tokenValidationSettings.ValidateLifetime,
                    ValidateIssuerSigningKey = tokenValidationSettings.ValidateIssuerSigningKey,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
            });

        services.AddAuthorization();

        services.AddScoped<RefreshTokenService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddSingleton<IJwtTokenService, JwtTokenService>();

        return services;
    }
}
