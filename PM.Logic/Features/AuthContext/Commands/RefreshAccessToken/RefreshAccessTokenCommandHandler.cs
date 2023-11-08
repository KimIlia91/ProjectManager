using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.AuthContext.Dtos;

namespace PM.Application.Features.AuthContext.Commands.RefreshAccessToken;

/// <summary>
/// Handles the command to refresh an access token using a refresh token.
/// </summary>
internal sealed class RefreshAccessTokenCommandHandler
    : IRequestHandler<RefreshAccessTokenCommand, ErrorOr<AuthResult>>
{
    private readonly IIdentityService _identityService;

    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshAccessTokenCommandHandler"/> class.
    /// </summary>
    /// <param name="identityService">The service responsible for identity-related operations.</param>
    public RefreshAccessTokenCommandHandler(
        IIdentityService identityService)
    {
        _identityService = identityService;
    }

    /// <summary>
    /// Handles the refresh of an access token using a provided refresh token.
    /// </summary>
    /// <param name="command">The command to refresh the access token.</param>
    /// <param name="cancellationToken">The token for cancelling the operation.</param>
    /// <returns>An <see cref="ErrorOr{AuthResult}"/> representing the result of the operation.</returns>
    public async Task<ErrorOr<AuthResult>> Handle(
        RefreshAccessTokenCommand command,
        CancellationToken cancellationToken)
    {
        return await _identityService
            .RefreshAccessTokenAsync(command.RefreshToken);
    }
}

