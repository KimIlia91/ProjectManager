using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.AuthContext.Dtos;

namespace PM.Application.Features.AuthContext.Commands.RefreshAccessToken;

internal sealed class RefreshAccessTokenCommandHandler
    : IRequestHandler<RefreshAccessTokenCommand, ErrorOr<AuthResult>>
{
    private readonly IIdentityService _identityService;

    public RefreshAccessTokenCommandHandler(
        IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<ErrorOr<AuthResult>> Handle(
        RefreshAccessTokenCommand command, 
        CancellationToken cancellationToken)
    {
        return await _identityService
            .RefreshAccessTokenAsync(command.RefreshToken);
    }
}
