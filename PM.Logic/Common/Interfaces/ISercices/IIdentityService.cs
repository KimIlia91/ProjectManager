using ErrorOr;
using PM.Application.Features.AuthContext.Dtos;
using PM.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace PM.Application.Common.Interfaces.ISercices;

/// <summary>
/// Service interface for managing identity-related operations.
/// </summary>
public interface IIdentityService
{
    /// <summary>
    /// Check if a user with the specified email exists.
    /// </summary>
    /// <param name="email">The email to check for existence.</param>
    /// <returns>A boolean indicating whether a user with the given email exists.</returns>
    Task<bool> IsEmailExistAsync(string email);

    /// <summary>
    /// Check if a role with the specified name exists.
    /// </summary>
    /// <param name="roleName">The name of the role to check for existence.</param>
    /// <returns>A boolean indicating whether a role with the given name exists.</returns>
    Task<bool> IsRoleExistAsync(string email);

    /// <summary>
    /// Register a new user with the specified password and role.
    /// </summary>
    /// <param name="password">The password for the new user.</param>
    /// <param name="roleName">The name of the role for the new user.</param>
    /// <param name="user">The user object to register.</param>
    /// <returns>An error or the registered user object.</returns>
    Task<ErrorOr<User>> RegisterAsync(
        string password,
        string roleName,
        User user);

    /// <summary>
    /// Update user information.
    /// </summary>
    /// <param name="employee">The user to update.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>An error or the updated user object.</returns>
    Task<ErrorOr<User>> UpdateAsync(
        User employee,
        CancellationToken cancellationToken);

    /// <summary>
    /// Login a user with the specified email and password.
    /// </summary>
    /// <param name="email">The user's email for login.</param>
    /// <param name="password">The user's password for login.</param>
    /// <returns>An error or an authentication result containing access and refresh tokens.</returns>
    Task<ErrorOr<AuthResult>> LoginAsync(
        string email,
        string password);

    /// <summary>
    /// Log out a user with the specified user ID.
    /// </summary>
    /// <param name="userId">The ID of the user to log out.</param>
    Task LogOutAsync(int userId);

    /// <summary>
    /// Refresh the access token using a refresh token.
    /// </summary>
    /// <param name="refreshToken">The refresh token to use for access token renewal.</param>
    /// <returns>An error or an authentication result containing a new access token.</returns>
    Task<ErrorOr<AuthResult>> RefreshAccessTokenAsync(string refreshToken);
}
