using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PM.Application.Common.Interfaces.ISercices;
using PM.Domain.Entities;
using PM.Infrastructure.Persistence;

namespace PM.Infrastructure.Identity.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<Employee> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly ApplicationDbContext _context;

    public IdentityService(
        UserManager<Employee> userManager,
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

    public async Task<ErrorOr<Employee>> RegisterAsync(
        string password,
        string roleName,
        Employee employee)
    {
        var resultUser = await _userManager.CreateAsync(employee, password);

        if (!resultUser.Succeeded)
            return Error.Failure("User could not be created");

        var resultRole = await _userManager.AddToRoleAsync(employee, roleName);

        if (resultRole.Succeeded)
            return employee;

        await _userManager.DeleteAsync(employee);
        return Error.Failure("User could not be created");
    }

    public async Task<ErrorOr<Employee>> UpdateAsync(
        Employee employee,
        CancellationToken cancellationToken)
    {
        var resultUser = await _userManager.UpdateAsync(employee);

        if (!resultUser.Succeeded)
            return Error.Failure("Employee could not be created");

        return employee;
    }
}
