using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.Application.Features.AuthContext.Commands.Login;
using PM.Application.Features.AuthContext.Commands.Logout;
using PM.Application.Features.AuthContext.Commands.RefreshAccessToken;
using PM.Application.Features.AuthContext.Dtos;

namespace PM.WebApi.Controllers;

/// <summary>
/// Controller for handling authentication-related actions.
/// </summary>
public class AuthController : ApiBaseController
{
    /// <summary>
    /// Login to get access token.
    /// </summary>
    /// <param name="command">The command for creating the project.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult representing the operation result, which can be one of the following:
    /// - 200 OK with the created project if successful.
    /// - A problem response with errors if the operation encounters issues.
    /// </returns>
    [AllowAnonymous]
    [HttpPost("Login")]
    [ProducesResponseType(typeof(AuthResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> LoginAsync(
        LoginCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    /// <summary>
    /// Logs out the currently authenticated user.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult indicating the result of the logout operation, which is:
    /// - 204 No Content: If the logout was successful and no further content is provided.
    /// </returns>
    [HttpPost("Logout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> LogoutAsync(
        CancellationToken cancellationToken)
    {
        var command = new LogoutCommand();
        await Mediator.Send(command, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Refreshes the user's access token.
    /// </summary>
    /// <param name="command">The command for refreshing the access token.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult representing the operation result, which can be one of the following:
    /// - 200 OK with the refreshed access token if successful.
    /// - A problem response with errors if the operation encounters issues.
    /// </returns>
    [HttpPost("RefreshAccessToken")]
    [ProducesResponseType(typeof(AuthResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshAccessTokenAsync(
        RefreshAccessTokenCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }
}
