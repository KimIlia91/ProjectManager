using Microsoft.AspNetCore.Identity;
using PM.Application.Common.Enums;
using PM.Domain.Common.Extensions;
using PM.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace PM.Infrastructure.Persistence.Seeds
{
    internal class UserSeed
    {
        public static async Task SeedAsync(
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            var userResult = User.Create(
                "Leader FirstName",
                "Leader LantName",
                "Leader@Leader.com");

            var user = userResult.Value;

            var userRole = await roleManager.FindByNameAsync(RoleEnum.Supervisor.GetDescription());
            var userExists = userManager.Users.Any(u => u.Email == user.Email);
            if (!userExists && userRole is not null)
            {
                await userManager.CreateAsync(user, "TestLeader123!");
                await userManager.AddToRoleAsync(user, userRole.Name);
            }
        }
    }
}
