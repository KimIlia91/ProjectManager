using ErrorOr;
using Microsoft.AspNetCore.Identity;
using PM.Application.Common.Identity.Models;
using PM.Application.Common.Interfaces.ISercices;
using PM.Domain.Entities;

namespace PM.Infrastructure.Services;

internal class ApplicationUserService : IApplicationUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public ApplicationUserService(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<ErrorOr<ApplicationUser>> RegistrAsync(
        string password,
        int roleId,
        Employee employee)
    {
        var user = await _userManager.FindByEmailAsync(employee.Email);

        if (user is not null)
            return Error.Conflict("Email already in use", nameof(employee.Email));

        user = new ApplicationUser()
        {
            UserName = employee.Email,
            Email = employee.Email,
            Employee = employee
        };

        var role = await _roleManager.FindByIdAsync(roleId.ToString());

        if (role is null)
            return Error.NotFound("Not found", nameof(roleId));

        var resultUser = await _userManager.CreateAsync(user, password);

        if (!resultUser.Succeeded)
            return Error.Failure("User could not be created");

        var resultRole = await _userManager.AddToRoleAsync(user, role.Name);

        if (!resultRole.Succeeded)
        {
            await _userManager.DeleteAsync(user);
            return Error.Failure("User could not be created");
        }

        return user;
    }
}
