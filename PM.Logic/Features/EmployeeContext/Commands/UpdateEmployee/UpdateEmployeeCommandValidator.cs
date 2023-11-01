using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Domain.Common.Constants;

namespace PM.Application.Features.EmployeeContext.Commands.UpdateEmployee;

public sealed class UpdateEmployeeCommandValidator
    : AbstractValidator<UpdateEmployeeCommand>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IIdentityService _identityService;

    public UpdateEmployeeCommandValidator(
        IEmployeeRepository employeeRepository,
        IIdentityService identityService)
    {
        _employeeRepository = employeeRepository;
        _identityService = identityService;

        RuleFor(command => command.Id)
            .NotEmpty()
            .MustAsync(EmployeeMustBeInDatabase);

        RuleFor(command => command.FirstName)
            .NotEmpty()
            .MaximumLength(EntityConstants.FirstName);

        RuleFor(command => command.LastName)
            .NotEmpty()
            .MaximumLength(EntityConstants.LastName);

        RuleFor(command => command.MiddelName)
            .MaximumLength(EntityConstants.MiddelName)
            .When(command => command.MiddelName is not null);

        RuleFor(command => command.RoleName)
            .NotEmpty()
            .MaximumLength(EntityConstants.RoleNameLength)
            .MustAsync(MustBeInDatabase);
    }

    private async Task<bool> MustBeInDatabase(
        string roleName, 
        CancellationToken token)
    {
        return await _identityService.IsRoleExistAsync(roleName);
    }

    private async Task<bool> EmployeeMustBeInDatabase(
        UpdateEmployeeCommand command,
        int id, 
        CancellationToken cancellationToken)
    {
        command.Employee = await _employeeRepository
            .GetOrDeafaultAsync(e => e.Id == command.Id, cancellationToken);

        return command.Employee is not null;
    }
}
