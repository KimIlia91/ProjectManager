using ErrorOr;
using Microsoft.AspNetCore.Identity;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.AuthContext.Dtos;
using PM.Domain.Common.Constants;
using PM.Domain.Entities;
using PM.Infrastructure.Auth.Services;
using Task = System.Threading.Tasks.Task;

namespace PM.Infrastructure.Identity.Services;

/// <summary>
/// Service for managing identity-related operations.
/// </summary>
public class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly RefreshTokenService _refreshTokenService;
    private readonly IJwtTokenService _jwtTokenService;

    /// <summary>
    /// Constructs an instance of the IdentityService.
    /// </summary>
    /// <param name="userManager">The user manager for managing users.</param>
    /// <param name="roleManager">The role manager for managing user roles.</param>
    /// <param name="refreshTokenService">Service for handling refresh tokens.</param>
    /// <param name="jwtTokenService">Service for handling JWT tokens.</param>
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

    /// <inheritdoc />
    public async Task<bool> IsEmailExistAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email) is not null;
    }

    /// <inheritdoc />
    public async Task<bool> IsRoleExistAsync(string roleName)
    {
        return await _roleManager.RoleExistsAsync(roleName);
    }

    /// <inheritdoc />
    public async Task<ErrorOr<User>> CreateUserAsync(
        string password,
        //string roleName,
        User user)
    {
        var resultUser = await _userManager.CreateAsync(user, password);

        if (!resultUser.Succeeded)
            return Error.Failure(resultUser.Errors.First().Description);

        var resultRole = await _userManager.AddToRolesAsync(user, 
            new List<string>(){ RoleConstants.Employee, RoleConstants.Manager });

        if (resultRole.Succeeded)
            return user;

        await _userManager.DeleteAsync(user);
        return Error.Failure("User could not be created");
    }

    /// <inheritdoc />
    public async Task<ErrorOr<User>> UpdateAsync(
        User user,
        CancellationToken cancellationToken)
    {
        var resultUser = await _userManager.UpdateAsync(user);

        if (!resultUser.Succeeded)
            return Error.Failure("Employee could not be created");

        return user;
    }

    /// <inheritdoc />
    public async Task<ErrorOr<AuthResult>> LoginAsync(
        string email,
        string password)
    {
        if (await _userManager.FindByEmailAsync(email) is not User user)
            return Error.Unauthorized("Login or password incorrect");

        if (!await _userManager.CheckPasswordAsync(user, password))
            return Error.Unauthorized("Login or password incorrect");

        var roles = await _userManager.GetRolesAsync(user);
        var accessToken = _jwtTokenService.GenerateToken(user, roles.ToList());
        var refreshToken = await _refreshTokenService.GenerateAsync(user);
        return new AuthResult(user.Email!, accessToken, refreshToken);
    }

    /// <inheritdoc />
    public async Task LogOutAsync(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        await _userManager.UpdateSecurityStampAsync(user);
    }

    /// <inheritdoc />
    public async Task<ErrorOr<AuthResult>> RefreshAccessTokenAsync(string refreshToken)
    {
        var validationResult = await _refreshTokenService.ValidateAsync(refreshToken);

        if (validationResult.IsError)
            return validationResult.Errors;

        var user = validationResult.Value;
        var roles = await _userManager.GetRolesAsync(user);
        var accessToken = _jwtTokenService.GenerateToken(user, roles.ToList());
        var newRefreshToken = await _refreshTokenService.GenerateAsync(user);
        return new AuthResult(user.Email!, accessToken, newRefreshToken);
    }
}
