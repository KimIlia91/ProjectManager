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

        await RoleSeed.SeedAsync(roleManager);
    }
}
