using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.EmployeeContext.Commands.CreateEmployee;
using PM.Domain.Common.Constants;

namespace PM.Application.Features.EmployeeContext.Commands.CreateEmployeel;

public sealed class CreateEmployeeCommandValidator
    : AbstractValidator<CreateEmployeeCommand>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IIdentityService _identityService;

    public CreateEmployeeCommandValidator(
        IRoleRepository roleRepository,
        IEmployeeRepository employeeRepository,
        IIdentityService identityService)
    {
        _identityService = identityService;
        _roleRepository = roleRepository;
        _employeeRepository = employeeRepository;

        RuleFor(command => command.FirstName)
            .NotEmpty()
            .MaximumLength(EntityConstants.FirstName);

        RuleFor(command => command.LastName)
            .NotEmpty()
            .MaximumLength(EntityConstants.LastName);

        RuleFor(command => command.MiddelName)
            .MaximumLength(EntityConstants.MiddelName)
            .When(command => command.MiddelName is not null);

        RuleFor(command => command.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(EntityConstants.Email)
            .MustAsync(MustBeUnique);

        RuleFor(command => command.RoleName)
            .NotEmpty()
            .MaximumLength(EntityConstants.RoleNameLength)
            .MustAsync(MustBeInDatabase);
    }

    private async Task<bool> MustBeInDatabase(
        string roleName,
        CancellationToken cancellationToken)
    {
        return await _identityService.IsRoleExistAsync(roleName);
    }

    private async Task<bool> MustBeUnique(
        string email,
        CancellationToken cancellationToken)
    {
        var emailExist = await _employeeRepository
          .GetOrDeafaultAsync(e => e.Email == email, cancellationToken);

        return emailExist is null;
    }
}
