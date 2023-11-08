using PM.WebApi.Common.Congifuratuions.Swagger;

namespace PM.WebApi.Common.Extensions;

public static class ApplicationBuilderExtensions
{
    public static WebApplication UseAppConfiguration(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseCustomSwaggerConfiguration();
        }

        app.UseExceptionHandler("/error");
        //app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}
