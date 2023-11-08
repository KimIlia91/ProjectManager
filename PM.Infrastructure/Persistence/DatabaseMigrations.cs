using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PM.Infrastructure.Persistence;

/// <summary>
/// A static class for database migration operations.
/// </summary>
public static class DatabaseMigrations
{
    /// <summary>
    /// Performs database migration asynchronously if the database provider is SQL Server.
    /// </summary>
    /// <param name="services">The IServiceProvider to obtain the ApplicationDbContext.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public static async Task MigrateAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        if (context.Database.IsSqlServer())
        {
            await context.Database.MigrateAsync();
        }
    }
}
