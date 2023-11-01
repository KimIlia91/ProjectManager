using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.EmployeeContext.Dtos;
using PM.Domain.Entities;

namespace PM.Application.Features.EmployeeContext.Commands.CreateEmployee;

public sealed class CreateEmployeeCommandHandler
    : IRequestHandler<CreateEmployeeCommand, ErrorOr<CreateEmployeeResult>>
{
    private readonly IIdentityService _employeeService;

    public CreateEmployeeCommandHandler(
        IIdentityService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task<ErrorOr<CreateEmployeeResult>> Handle(
        CreateEmployeeCommand command,
        CancellationToken cancellationToken)
    {
        var result = Employee.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            command.MiddelName);

        if (result.IsError)
            return result.Errors;

        var registerResult = await _employeeService
            .RegisterAsync(command.Password, command.RoleName, result.Value);

        if (registerResult.IsError)
            return registerResult.Errors;

        return new CreateEmployeeResult(registerResult.Value.Id);
    }
}
