using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PM.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace PM.Infrastructure.Persistence.Seeds;

/// <summary>
/// A static class responsible for seeding the database with initial data.
/// </summary>
public static class DatabaseSeeds
{
    /// <summary>
    /// Adds initial seed data to the database.
    /// </summary>
    /// <param name="services">The service provider containing required services.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task AddSeeds(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<Role>>();
        var userManager = services.GetRequiredService<UserManager<User>>();

        await RoleSeed.SeedAsync(roleManager);
        await UserSeed.SeedAsync(userManager, roleManager);
    }
}
