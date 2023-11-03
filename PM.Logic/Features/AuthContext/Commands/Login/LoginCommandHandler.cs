using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.AuthContext.Dtos;

namespace PM.Application.Features.AuthContext.Commands.Login;

internal sealed class LoginCommandHandler
    : IRequestHandler<LoginCommand, ErrorOr<LoginResult>>
{
    private readonly IIdentityService _identityService;

    public LoginCommandHandler(
        IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<ErrorOr<LoginResult>> Handle(
        LoginCommand command, 
        CancellationToken cancellationToken)
    {
        return await _identityService.LoginAsync(command.Email, command.Password);
    }
}
