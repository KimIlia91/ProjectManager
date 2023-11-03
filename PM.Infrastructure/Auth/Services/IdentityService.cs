using ErrorOr;
using Microsoft.AspNetCore.Identity;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.AuthContext.Dtos;
using PM.Domain.Entities;
using PM.Infrastructure.Auth.Services;
using Task = System.Threading.Tasks.Task;

namespace PM.Infrastructure.Identity.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly RefreshTokenService _refreshTokenService;
    private readonly IJwtTokenService _jwtTokenService;

    public IdentityService(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        RefreshTokenService refreshTokenService,
        IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _refreshTokenService = refreshTokenService;
        _jwtTokenService = jwtTokenService;
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

    public async Task<ErrorOr<AuthResult>> LoginAsync(
        string email,
        string password)
    {
        if (await _userManager.FindByEmailAsync(email) is not User user)
            return Error.Unauthorized("Login or password incorrect");

        if (!await _userManager.CheckPasswordAsync(user, password))
            return Error.Unauthorized("Login or password incorrect");

        var accessToken = _jwtTokenService.GenerateToken(user);
        var refreshToken = await _refreshTokenService.GenerateAsync(user);
        return new AuthResult(user.Email!, accessToken, refreshToken);
    }

    public async Task LogOutAsync(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        await _userManager.UpdateSecurityStampAsync(user);
    }

    public async Task<ErrorOr<AuthResult>> RefreshAccessTokenAsync(string refreshToken)
    {
        var validationResult = await _refreshTokenService.ValidateAsync(refreshToken);

        if (validationResult.IsError)
            return validationResult.Errors;

        var user = validationResult.Value;
        var accessToken = _jwtTokenService.GenerateToken(user);
        var newRefreshToken = await _refreshTokenService.GenerateAsync(user);
        return new AuthResult(user.Email!, accessToken, newRefreshToken);
    }
}
