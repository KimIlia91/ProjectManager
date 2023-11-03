using Microsoft.AspNetCore.Mvc.Infrastructure;
using PM.Application;
using PM.Infrastructure;
using PM.Infrastructure.Persistence.Seeds;
using PM.WebApi.Common.Congifuratuions.Swagger;
using PM.WebApi.Common.Errors;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddSingleton<ProblemDetailsFactory, PmErrorProblemDitailsFactory>();
builder.Services.AddApplication();
//builder.Services.AddSwaggerGen();
builder.Services.AddCustomSwaggerGen(Assembly.GetExecutingAssembly());

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseCustomSwaggerConfiguration();
}

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
await DataBaseSeeds.AddSeeds(services);

app.Run();
