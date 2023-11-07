using Microsoft.AspNetCore.Identity;
using PM.Application.Common.Enums;
using PM.Domain.Common.Enums;
using PM.Domain.Common.Extensions;
using PM.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace PM.Infrastructure.Persistence.Seeds;

/// <summary>
/// A static class responsible for seeding roles in the application.
/// </summary>
public static class RoleSeed
{
    /// <summary>
    /// Seeds roles into the application using the provided role manager.
    /// </summary>
    /// <param name="roleManager">The role manager to create roles.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(RoleManager<Role> roleManager)
    {
        var roles = EnumExtensions.GetAllAsEnumerable<RoleEnum>();

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role.GetDescription()))
            {
                await roleManager.CreateAsync(new Role { Name = role.GetDescription() });
            }
        }
    }
}
