using ErrorOr;
using MapsterMapper;
using MediatR;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.UserContext.Dtos;
using PM.Domain.Entities;

namespace PM.Application.Features.UserContext.Commands.Register;

public sealed class RegisterCommandHandler 
    : IRequestHandler<RegisterCommand, ErrorOr<RegisterResult>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationUserService _userService;

    public RegisterCommandHandler(
        IApplicationUserService userService,
        IMapper mapper)
    {
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<ErrorOr<RegisterResult>> Handle(
        RegisterCommand command, 
        CancellationToken cancellationToken)
    {
        var employeeResult = Employee.Create(
           command.FirstName,
           command.LastName,
           command.Email,
           command.MiddelName);

        if (employeeResult.IsError)
            return employeeResult.Errors;

        var registerResult = await _userService
            .RegistrAsync(command.Password, command.RoleId, employeeResult.Value);

        if (registerResult.IsError)
            return registerResult.Errors;

        return _mapper.Map<RegisterResult>(employeeResult.Value);
    }
}
