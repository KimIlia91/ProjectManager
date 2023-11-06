using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Resources;
using PM.Domain.Common.Constants;

namespace PM.Application.Features.UserContext.Commands.UpdateUser;

/// <summary>
/// Validates the update user command.
/// </summary>
public sealed class UpdateUserCommandValidator
    : AbstractValidator<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUserCommandValidator"/> class.
    /// </summary>
    /// <param name="userRepository">The repository for user data.</param>
    /// <param name="roleRepository">The repository for role data.</param>
    public UpdateUserCommandValidator(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(EmployeeMustBeInDatabase)
            .WithMessage(ErrorsResource.NotFound);

        RuleFor(command => command.FirstName)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MaximumLength(EntityConstants.FirstName)
            .WithMessage(string.Format(ErrorsResource.MaxLength, EntityConstants.FirstName));

        RuleFor(command => command.LastName)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MaximumLength(EntityConstants.LastName)
            .WithMessage(string.Format(ErrorsResource.MaxLength, EntityConstants.LastName));

        RuleFor(command => command.MiddelName)
            .MaximumLength(EntityConstants.MiddelName)
            .When(command => command.MiddelName is not null)
            .WithMessage(string.Format(ErrorsResource.MaxLength, EntityConstants.MiddelName));

        RuleFor(command => command.Email)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .EmailAddress()
            .WithMessage(ErrorsResource.InvalidEmail)
            .MaximumLength(EntityConstants.Email)
            .WithMessage(string.Format(ErrorsResource.MaxLength, EntityConstants.Email))
            .MustAsync(EmailMustBeInUnique)
            .WithMessage(ErrorsResource.NotFound);
    }

    private async Task<bool> EmailMustBeInUnique(
        UpdateUserCommand command,
        string email,
        CancellationToken cancellationToken)
    {
        var emailExist = await _userRepository
            .GetOrDeafaultAsync(e => e.Email == email && e.Id != command.Id, cancellationToken);

        return emailExist is null;
    }

    private async Task<bool> EmployeeMustBeInDatabase(
        UpdateUserCommand command,
        int id,
        CancellationToken cancellationToken)
    {
        command.User = await _userRepository
            .GetOrDeafaultAsync(e => e.Id == command.Id, cancellationToken);

        return command.User is not null;
    }
}
