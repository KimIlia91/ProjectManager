using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;

namespace PM.Application.Features.UserContext.Commands.DeleteUser;

public sealed class DeleteUserCommandValidator
    : AbstractValidator<DeleteUserCommand>
{
    private readonly IUserRepository _employeeRepository;

    public DeleteUserCommandValidator(
        IUserRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;

        RuleFor(command => command.EmployeeId)
            .NotEmpty()
            .MustAsync(MustBeInDatabase);
    }

    private async Task<bool> MustBeInDatabase(
        DeleteUserCommand command,
        int id,
        CancellationToken cancellationToken)
    {
        command.Employee = await _employeeRepository
            .GetOrDeafaultAsync(e => e.Id == id, cancellationToken);

        return command.Employee is not null;
    }
}
