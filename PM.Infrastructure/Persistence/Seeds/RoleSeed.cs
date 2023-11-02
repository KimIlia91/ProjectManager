using Microsoft.AspNetCore.Identity;
using PM.Application.Common.Enums;
using PM.Domain.Common.Extensions;
using PM.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace PM.Infrastructure.Persistence.Seeds;

public static class RoleSeed
{
    public static async Task SeedAsync(RoleManager<Role> roleManager)
    {
        var roles = EnumExtensions.ToEnumResults<RoleEnum>();

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role.Name))
            {
                await roleManager.CreateAsync(new Role { Name = role.Name });
            }
        }
    }
}
