using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Resources;

namespace PM.Application.Features.UserContext.Commands.DeleteUser;

/// <summary>
/// Validator for the delete user command.
/// </summary>
public sealed class DeleteUserCommandValidator
    : AbstractValidator<DeleteUserCommand>
{
    private readonly IUserRepository _employeeRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteUserCommandValidator"/> class.
    /// </summary>
    /// <param name="employeeRepository">The repository for users.</param>
    public DeleteUserCommandValidator(
        IUserRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;

        RuleFor(command => command.UserId)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(MustBeInDatabase)
            .WithMessage(ErrorsResource.NotFound);
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
