using FluentValidation;
using PM.Domain.Common.Constants;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.EmployeeContext.Commands.CreateEmployee;

namespace PM.Application.Features.EmployeeContext.Commands.CreateEmployeel;

public sealed class CreateEmployeeCommandValidator
    : AbstractValidator<CreateEmployeeCommand>
{
    private readonly IIdentityService _identityService;

    public CreateEmployeeCommandValidator(
        IIdentityService identityService)
    {
        _identityService = identityService;

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
        CancellationToken token)
    {
        return await _identityService.IsRoleExistAsync(roleName);
    }

    private async Task<bool> MustBeUnique(
        string email,
        CancellationToken cancellationToken)
    {
        return !await _identityService.IsEmailExistAsync(email);
    }
}
