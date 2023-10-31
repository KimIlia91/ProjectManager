using FluentValidation;
using PM.Domain.Common.Constants;

namespace PM.Application.Features.UserContext.Commands.Register;

public sealed class RegisterCommandValidator
    : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        //TODO: Дописать валидацию
        RuleFor(command => command.Email)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(EntityConstants.Email);

        RuleFor(command => command.FirstName)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .MaximumLength(EntityConstants.FirstName);

        RuleFor(command => command.LastName)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .MaximumLength(EntityConstants.LastName);

        RuleFor(command => command.MiddelName)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .MaximumLength(EntityConstants.MiddelName)
            .When(command => command.MiddelName is not null);

        RuleFor(command => command.Password)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .MinimumLength(EntityConstants.PasswordMinLength)
            .MaximumLength(EntityConstants.PasswordMaxLength);
    }
}
