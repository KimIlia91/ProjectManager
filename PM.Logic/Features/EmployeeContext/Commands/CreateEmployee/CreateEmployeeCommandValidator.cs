using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.EmployeeContext.Commands.CreateEmployee;
using PM.Domain.Common.Constants;

namespace PM.Application.Features.EmployeeContext.Commands.CreateEmployeel;

public sealed class CreateEmployeeCommandValidator
    : AbstractValidator<CreateEmployeeCommand>
{
    private readonly IEmployeeRepository _employeeRepository;

    public CreateEmployeeCommandValidator(
        IEmployeeRepository employeeRepository)
    {
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
    }

    private async Task<bool> MustBeUnique(
        string email, 
        CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository
            .GetOrDeafaultAsync(e => e.Email == email, cancellationToken);

        return employee is null;
    }
}
