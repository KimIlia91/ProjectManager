using Microsoft.AspNetCore.Mvc.Infrastructure;
using PM.Application;
using PM.Infrastructure;
using PM.WebApi.Common.Congifuratuions.Swagger;
using PM.WebApi.Common.Errors;
using System.Reflection;

namespace PM.WebApi.Common.Extensions;

public static class WebApplicationExtensions
{
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
