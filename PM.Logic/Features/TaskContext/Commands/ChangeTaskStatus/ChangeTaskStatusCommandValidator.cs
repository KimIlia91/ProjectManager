using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Resources;
using PM.Domain.Common.Constants;

namespace PM.Application.Features.TaskContext.Commands.ChangeTaskStatus;

public sealed class ChangeTaskStatusCommandValidator
    : AbstractValidator<ChangeTaskStatusCommand>
{
    private readonly ITaskRepository _taskRepository;

    public ChangeTaskStatusCommandValidator(
        ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;

        RuleFor(command => command.TaskId)
            .NotEmpty()
            .WithMessage(ErrorsResource.NotFound)
            .MustAsync(TaskMustBeInDatabase)
            .WithMessage(ErrorsResource.NotFound);

        RuleFor(command => command.Status)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MaximumLength(EntityConstants.EnumStatusLength)
            .WithMessage(string.Format(ErrorsResource.MaxLength, EntityConstants.EnumStatusLength));
    }

    private async Task<bool> TaskMustBeInDatabase(
        ChangeTaskStatusCommand command,
        int taskId, 
        CancellationToken cancellationToken)
    {
        command.Task = await _taskRepository
            .GetOrDeafaultAsync(t => t.Id == taskId, cancellationToken);

        return command.Task is not null;
    }
}
