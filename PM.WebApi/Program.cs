using PM.Infrastructure.Persistence;
using PM.Infrastructure.Persistence.Seeds;
using PM.WebApi.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

var app = builder.Build();
app.UseAppConfiguration();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
await DatabaseMigrations.MigrateAsync(services);
await DatabaseSeeds.AddSeeds(services);

app.Run();
