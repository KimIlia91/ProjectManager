using Microsoft.AspNetCore.Identity;
using PM.Application.Common.Enums;
using PM.Domain.Common.Enums;
using PM.Domain.Common.Extensions;
using PM.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace PM.Infrastructure.Persistence.Seeds;

/// <summary>
/// A class responsible for seeding a user with a specific role in the application.
/// </summary>
internal class UserSeed
{
    /// <summary>
    /// Seeds a user with a specified role into the application using the provided user and role managers.
    /// </summary>
    /// <param name="userManager">The user manager to create and manage users.</param>
    /// <param name="roleManager">The role manager to manage user roles.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(
        UserManager<User> userManager,
        RoleManager<Role> roleManager)
    {
        var userResult = User.Create(
            "Supervisor FirstName",
            "Supervisor LantName",
            "Supervisor@Supervisor.com");

        var user = userResult.Value;

        var userRole = await roleManager.FindByNameAsync(RoleEnum.Supervisor.GetDescription());
        var userExists = userManager.Users.Any(u => u.Email == user.Email);
        if (!userExists && userRole is not null)
        {
            await userManager.CreateAsync(user, "Test123!");
            await userManager.AddToRoleAsync(user, userRole.Name);
        }
    }
}
