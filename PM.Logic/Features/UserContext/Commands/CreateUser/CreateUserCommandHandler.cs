using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.EmployeeContext.Dtos;
using PM.Domain.Entities;

namespace PM.Application.Features.UserContext.Commands.CreateUser;

public sealed class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand, ErrorOr<CreateUserResult>>
{
    private readonly IIdentityService _identityService;

    public CreateUserCommandHandler(
        IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<ErrorOr<CreateUserResult>> Handle(
        CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        var result = User.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            command.MiddelName);

        if (result.IsError)
            return result.Errors;

        var registerResult = await _identityService
            .RegisterAsync(command.Password, command.RoleName, result.Value);

        if (registerResult.IsError)
            return registerResult.Errors;

        return new CreateUserResult(registerResult.Value.Id);
    }
}
