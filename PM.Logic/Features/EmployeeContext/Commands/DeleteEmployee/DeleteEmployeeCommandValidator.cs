using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;

namespace PM.Application.Features.EmployeeContext.Commands.DeleteEmployee;

public sealed class DeleteEmployeeCommandValidator
    : AbstractValidator<DeleteEmployeeCommand>
{
    private readonly IEmployeeRepository _employeeRepository;

    public DeleteEmployeeCommandValidator(
        IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;

        RuleFor(command => command.EmployeeId)
            .NotEmpty()
            .MustAsync(MustBeInDatabase);
    }

    private async Task<bool> MustBeInDatabase(
        DeleteEmployeeCommand command,
        int id,
        CancellationToken cancellationToken)
    {
        command.Employee = await _employeeRepository
            .GetOrDeafaultAsync(e => e.Id == id, cancellationToken);

        return command.Employee is not null;
    }
}
