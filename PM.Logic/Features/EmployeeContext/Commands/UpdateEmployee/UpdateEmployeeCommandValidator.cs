using FluentValidation;
using PM.Domain.Common.Constants;

namespace PM.Application.Features.EmployeeContext.Commands.UpdateEmployee;

internal sealed class UpdateEmployeeCommandValidator 
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
            .Null()
            .When(command => command.MiddelName != null)
            .MinimumLength(EntityConstants.MiddelName);

        RuleFor(command => command.Email)
            .NotEmpty()
            .EmailAddress()
            .MinimumLength(EntityConstants.Email);
    }
}
