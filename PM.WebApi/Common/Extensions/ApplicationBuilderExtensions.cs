using PM.WebApi.Common.Congifuratuions.Swagger;

namespace PM.WebApi.Common.Extensions;

/// <summary>
/// A static class containing extension methods for configuring the application.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Extension method to configure the application.
    /// </summary>
    /// <param name="app">The WebApplication instance.</param>
    /// <returns>The configured WebApplication instance.</returns>
    public static WebApplication UseAppConfiguration(this WebApplication app)
    {
        app.UseCustomSwaggerConfiguration();
        app.UseExceptionHandler("/error");
        // app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}
