using MediatR;
using PM.Application.Common.Interfaces.ISercices;

namespace PM.Application.Features.AuthContext.Commands.Logout;

internal sealed class LogoutCommandHandler
    : IRequestHandler<LogoutCommand>
{
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUserService;

    public LogoutCommandHandler(
        IIdentityService identityService, 
        ICurrentUserService currentUserService)
    {
        _identityService = identityService;
        _currentUserService = currentUserService;

    }

    public async Task Handle(
        LogoutCommand command, 
        CancellationToken cancellationToken)
    {
        await _identityService.LogOutAsync(_currentUserService.UserId);
    }
}
