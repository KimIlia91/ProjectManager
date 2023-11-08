using MediatR;
using PM.Application.Common.Interfaces.ISercices;

namespace PM.Application.Features.AuthContext.Commands.Logout;

/// <summary>
/// Handles the log-out command.
/// </summary>
internal sealed class LogoutCommandHandler
    : IRequestHandler<LogoutCommand>
{
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUserService;

    /// <summary>
    /// Initializes a new instance of the <see cref="LogoutCommandHandler"/> class.
    /// </summary>
    /// <param name="identityService">The identity service used for log-out.</param>
    /// <param name="currentUserService">The service for retrieving the current user's information.</param>
    public LogoutCommandHandler(
        IIdentityService identityService,
        ICurrentUserService currentUserService)
    {
        _identityService = identityService;
        _currentUserService = currentUserService;
    }

    /// <summary>
    /// Handles the log-out command asynchronously.
    /// </summary>
    /// <param name="command">The log-out command to handle.</param>
    /// <param name="cancellationToken">The token for canceling the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task Handle(
        LogoutCommand command,
        CancellationToken cancellationToken)
    {
        await _identityService.LogOutAsync(_currentUserService.UserId);
    }
}
