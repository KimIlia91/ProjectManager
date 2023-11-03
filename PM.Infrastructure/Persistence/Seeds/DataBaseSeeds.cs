using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PM.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace PM.Infrastructure.Persistence.Seeds;

public static class DataBaseSeeds
{
    public static async Task AddSeeds(IServiceProvider services)
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var roleManager = services.GetRequiredService<RoleManager<Role>>();
        var userManager = services.GetRequiredService<UserManager<User>>();

        await RoleSeed.SeedAsync(roleManager);
        await UserSeed.SeedAsync(userManager, roleManager);
    }
}
