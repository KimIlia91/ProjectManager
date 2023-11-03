using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.AuthContext.Dtos;

namespace PM.Application.Features.AuthContext.Commands.Login;

/// <summary>
/// Handler for the LoginCommand, responsible for authenticating a user.
/// </summary>
internal sealed class LoginCommandHandler
    : IRequestHandler<LoginCommand, ErrorOr<AuthResult>>
{
    private readonly IIdentityService _identityService;

    /// <summary>
    /// Initializes a new instance of the LoginCommandHandler.
    /// </summary>
    /// <param name="identityService">The service responsible for identity-related operations.</param>
    public LoginCommandHandler(
        IIdentityService identityService)
    {
        _identityService = identityService;
    }

    /// <summary>
    /// Handles the LoginCommand to authenticate a user.
    /// </summary>
    /// <param name="command">The LoginCommand containing user credentials.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An ErrorOr<LoginResult> representing the result of the authentication process.</returns>
    public async Task<ErrorOr<AuthResult>> Handle(
        LoginCommand command, 
        CancellationToken cancellationToken)
    {
        return await _identityService.LoginAsync(command.Email, command.Password);
    }
}
