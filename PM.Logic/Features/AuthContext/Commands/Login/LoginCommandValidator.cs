using FluentValidation;
using PM.Application.Common.Interfaces.ISercices;
using PM.Domain.Common.Constants;

namespace PM.Application.Features.AuthContext.Commands.Login;

public sealed class LoginCommandValidator
    : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(command => command.Password)
            .NotEmpty()
            .MinimumLength(EntityConstants.PasswordMinLength)
            .MaximumLength(EntityConstants.PasswordMaxLength);
    }
}
