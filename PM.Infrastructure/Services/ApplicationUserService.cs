using ErrorOr;
using Microsoft.AspNetCore.Identity;
using PM.Application.Common.Identity.Models;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.UserContext.Commands.Register;

namespace PM.Infrastructure.Services;

internal class ApplicationUserService : IApplicationUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationUserService(
        UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ErrorOr<ApplicationUser>> RegistrAsync(
        RegisterCommand command)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);

        if (user is not null)
            return Error.Conflict("Email already in use", nameof(command.Email));

        if (!command.Password.Equals(command.ConfirmPassword))
            return Error.Validation("Not match", nameof(command.ConfirmPassword));

        user = new ApplicationUser()
        {
            UserName = command.Email,
            Email = command.Email,
            PhoneNumber = command.PhoneNumber
        };

        var resultUser = await _userManager.CreateAsync(user, command.Password);

        if (!resultUser.Succeeded)
            return Error.Failure("User could not be created", nameof(command));

        await _userManager.AddToRoleAsync(user, command.RoleName);

        return user;
    }
}
