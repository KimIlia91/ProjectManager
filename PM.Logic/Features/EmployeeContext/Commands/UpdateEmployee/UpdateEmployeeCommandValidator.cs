using FluentValidation;
using PM.Domain.Common.Constants;

namespace PM.Application.Features.EmployeeContext.Commands.UpdateEmployee;

public sealed class UpdateEmployeeCommandValidator 
    : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();

        RuleFor(command => command.FirstName)
            .NotEmpty()
            .MinimumLength(EntityConstants.FirstName);

        RuleFor(command => command.LastName)
            .NotEmpty()
            .MinimumLength(EntityConstants.LastName);

        RuleFor(command => command.MiddelName)
            .MaximumLength(EntityConstants.MiddelName)
            .When(command => command.MiddelName is not null);

        RuleFor(command => command.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(EntityConstants.Email);
    }
}
