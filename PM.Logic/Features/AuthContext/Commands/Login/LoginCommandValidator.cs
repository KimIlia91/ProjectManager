using FluentValidation;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Resources;
using PM.Domain.Common.Constants;

namespace PM.Application.Features.AuthContext.Commands.Login;

/// <summary>
/// Validator for the LoginCommand, responsible for validating user login credentials.
/// </summary>
public sealed class LoginCommandValidator
    : AbstractValidator<LoginCommand>
{
    /// <summary>
    /// Initializes a new instance of the LoginCommandValidator.
    /// </summary>
    public LoginCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .EmailAddress()
            .WithMessage(ErrorsResource.InvalidEmail);

        RuleFor(command => command.Password)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MinimumLength(EntityConstants.PasswordMinLength)
            .WithMessage(string.Format(ErrorsResource.MinLength, EntityConstants.PasswordMinLength))
            .MaximumLength(EntityConstants.PasswordMaxLength)
            .WithMessage(string.Format(ErrorsResource.MaxLength, EntityConstants.PasswordMinLength));
    }
}
