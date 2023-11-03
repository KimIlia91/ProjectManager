using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.Application.Features.AuthContext.Commands.Login;
using PM.Application.Features.AuthContext.Commands.Logout;
using PM.Application.Features.AuthContext.Commands.RefreshAccessToken;

namespace PM.WebApi.Controllers
{
    public class AuthController : ApiBaseController
    {
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(
            LoginCommand command,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> LogoutAsync(
            CancellationToken cancellationToken)
        {
            var command = new LogoutCommand();
            await Mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpPost("RefreshAccessToken")]
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
}
