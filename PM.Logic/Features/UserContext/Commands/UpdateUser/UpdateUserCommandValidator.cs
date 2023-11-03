using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Domain.Common.Constants;

namespace PM.Application.Features.UserContext.Commands.UpdateUser;

public sealed class UpdateUserCommandValidator
    : AbstractValidator<UpdateUserCommand>
{
    private readonly IUserRepository _employeeRepository;
    private readonly IRoleRepository _roleRepository;

    public UpdateUserCommandValidator(
        IUserRepository employeeRepository,
        IRoleRepository roleRepository)
    {
        _employeeRepository = employeeRepository;
        _roleRepository = roleRepository;

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

        RuleFor(command => command.RoleName)
            .NotEmpty()
            .MaximumLength(EntityConstants.RoleNameLength)
            .MustAsync(EmailMustBeInUnique);
    }

    private async Task<bool> EmailMustBeInUnique(
        UpdateUserCommand command,
        string email,
        CancellationToken cancellationToken)
    {
        var emailExist = await _employeeRepository
            .GetOrDeafaultAsync(e => e.Email == email && e.Id != command.Id, cancellationToken);

        return emailExist is null;
    }

    private async Task<bool> MustBeInDatabase(
        string roleName,
        CancellationToken cancellationToken)
    {
        var role = await _roleRepository
            .GetOrDeafaultAsync(r => r.Name == roleName, cancellationToken);

        return role is not null;
    }

    private async Task<bool> EmployeeMustBeInDatabase(
        UpdateUserCommand command,
        int id,
        CancellationToken cancellationToken)
    {
        command.Employee = await _employeeRepository
            .GetOrDeafaultAsync(e => e.Id == command.Id, cancellationToken);

        return command.Employee is not null;
    }
}
