using ErrorOr;
using MapsterMapper;
using MediatR;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.EmployeeContext.Dtos;

namespace PM.Application.Features.UserContext.Commands.UpdateUser;

public sealed class UpdateUserCommandHandler
    : IRequestHandler<UpdateUserCommand, ErrorOr<UpdateUserResult>>
{
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public UpdateUserCommandHandler(
        IMapper mapper,
        IIdentityService identityService)
    {
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<ErrorOr<UpdateUserResult>> Handle(
        UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        command.Employee!.Update(
            command.FirstName,
            command.LastName,
            command.MiddelName,
            command.Email);

        var result = await _identityService
            .UpdateAsync(command.Employee, cancellationToken);

        if (result.IsError)
            return result.Errors;

        return _mapper.Map<UpdateUserResult>((result.Value, command.RoleName));
    }
}
