using ErrorOr;
using MapsterMapper;
using MediatR;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.EmployeeContext.Dtos;

namespace PM.Application.Features.UserContext.Commands.UpdateUser;

/// <summary>
/// Handles the command to update user information.
/// </summary>
public sealed class UpdateUserCommandHandler
    : IRequestHandler<UpdateUserCommand, ErrorOr<UpdateUserResult>>
{
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUserCommandHandler"/> class.
    /// </summary>
    /// <param name="mapper">The AutoMapper instance for mapping objects.</param>
    /// <param name="identityService">The identity service for managing user identity.</param>
    public UpdateUserCommandHandler(
        IMapper mapper,
        IIdentityService identityService)
    {
        _mapper = mapper;
        _identityService = identityService;
    }

    /// <summary>
    /// Handles the command to update user information.
    /// </summary>
    /// <param name="command">The command to update user information.</param>
    /// <param name="cancellationToken">The token to cancel the operation.</param>
    /// <returns>Returns the result of the user update operation.</returns>
    public async Task<ErrorOr<UpdateUserResult>> Handle(
        UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        command.User!.Update(
            command.FirstName,
            command.LastName,
            command.MiddelName,
            command.Email);

        var result = await _identityService
            .UpdateAsync(command.User, cancellationToken);

        if (result.IsError)
            return result.Errors;

        return _mapper.Map<UpdateUserResult>((result.Value));
    }
}
