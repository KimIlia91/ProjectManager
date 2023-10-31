using FluentValidation;
using PM.Domain.Common.Constants;

namespace PM.Application.Features.ProjectContext.Commands.CreateProject;

public sealed class CreateProjectCommandValidator 
    : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .MaximumLength(EntityConstants.ProjectName);

        RuleFor(command => command.CustomerCompanyId)
            .NotEmpty();

        RuleFor(command => command.ExecutorCompanyId)
            .NotEmpty();

        RuleFor(command => command.StartDate)
            .NotEmpty()
            .Must((command, startDate) => startDate <= command.EndDate)
            .WithMessage("Дата начала должна быть меньше или равна дате окончания");

        RuleFor(command => command.EndDate)
            .NotEmpty()
            .Must((command, endDate) => endDate >= command.StartDate)
            .WithMessage("Дата окончания должна быть больше или равна дате начала");

        RuleFor(command => command.Priority)
            .NotEmpty()
            .IsInEnum()
            .WithMessage("Выбран недопустимый приоритет");
    }
}
