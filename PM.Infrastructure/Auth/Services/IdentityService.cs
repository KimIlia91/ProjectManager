using ErrorOr;
using Microsoft.AspNetCore.Identity;
using PM.Application.Common.Interfaces.ISercices;
using PM.Domain.Entities;
using PM.Infrastructure.Persistence;

namespace PM.Infrastructure.Identity.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly ApplicationDbContext _context;

    public IdentityService(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        ApplicationDbContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }

    public async Task<bool> IsEmailExistAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email) is not null;
    }

    public async Task<bool> IsRoleExistAsync(string roleName)
    {
        return await _roleManager.RoleExistsAsync(roleName);
    }

    public async Task<ErrorOr<User>> RegisterAsync(
        string password,
        string roleName,
        User user)
    {
        var resultUser = await _userManager.CreateAsync(user, password);

        if (!resultUser.Succeeded)
            return Error.Failure("User could not be created");

        var resultRole = await _userManager.AddToRoleAsync(user, roleName);

        if (resultRole.Succeeded)
            return user;

        await _userManager.DeleteAsync(user);
        return Error.Failure("User could not be created");
    }

    public async Task<ErrorOr<User>> UpdateAsync(
        User user,
        CancellationToken cancellationToken)
    {
        var resultUser = await _userManager.UpdateAsync(user);

        if (!resultUser.Succeeded)
            return Error.Failure("Employee could not be created");

        return user;
    }
}
