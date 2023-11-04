using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Resources;
using PM.Domain.Common.Constants;

namespace PM.Application.Features.UserContext.Commands.CreateUser;

/// <summary>
/// Validates the CreateUserCommand input to ensure the provided 
/// user information is correct and follows the required rules.
/// </summary>
public sealed class CreateUserCommandValidator
    : AbstractValidator<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IIdentityService _identityService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserCommandValidator"/> class.
    /// </summary>
    /// <param name="userRepository">The repository for employees (users).</param>
    /// <param name="identityService">The identity service for user validation.</param>
    public CreateUserCommandValidator(
        IUserRepository userRepository,
        IIdentityService identityService)
    {
        _identityService = identityService;
        _userRepository = userRepository;

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
            .MustAsync(MustBeUnique)
            .WithMessage(ErrorsResource.NotFound);
    }

    private async Task<bool> MustBeUnique(
        string email,
        CancellationToken cancellationToken)
    {
        var emailExist = await _userRepository
          .GetOrDeafaultAsync(e => e.Email == email, cancellationToken);

        return emailExist is null;
    }
}
