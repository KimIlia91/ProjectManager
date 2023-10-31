using FluentValidation;

namespace PM.Application.Features.TaskContext.Commands.DeleteTask;

public sealed class DeleteTaskCommandValidator 
    : AbstractValidator<DeleteTaskCommand>
{
    public DeleteTaskCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();
    }
}
