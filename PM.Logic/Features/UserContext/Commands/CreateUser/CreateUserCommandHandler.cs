using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.EmployeeContext.Dtos;
using PM.Domain.Entities;

namespace PM.Application.Features.UserContext.Commands.CreateUser;

/// <summary>
/// Handles the command to create a new user.
/// </summary>
public sealed class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand, ErrorOr<CreateUserResult>>
{
    private readonly IIdentityService _identityService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserCommandHandler"/> class.
    /// </summary>
    /// <param name="identityService">The identity service used for user registration.</param>
    public CreateUserCommandHandler(
        IIdentityService identityService)
    {
        _identityService = identityService;
    }

    /// <summary>
    /// Handles the creation of a new user and registration.
    /// </summary>
    /// <param name="command">The user creation command.</param>
    /// <param name="cancellationToken">The token to cancel the operation.</param>
    /// <returns>A result indicating the outcome of the user creation and registration.</returns>
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
