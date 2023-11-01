using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;

namespace PM.Application.Features.TaskContext.Commands.DeleteTask;

public sealed class DeleteTaskCommandValidator 
    : AbstractValidator<DeleteTaskCommand>
{
    private readonly ITaskRepository _taskRepository;

    public DeleteTaskCommandValidator(
        ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;

        RuleFor(command => command.Id)
            .NotEmpty()
            .MustAsync(MustBeInDatabase);
    }

    private async Task<bool> MustBeInDatabase(
        DeleteTaskCommand command,
        int id, 
        CancellationToken cancellationToken)
    {
        command.Task = await _taskRepository
             .GetOrDeafaultAsync(t => t.Id == id, cancellationToken);

        return command.Task is not null;
    }
}
