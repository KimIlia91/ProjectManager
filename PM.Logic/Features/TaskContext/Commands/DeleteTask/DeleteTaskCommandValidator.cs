using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Resources;

namespace PM.Application.Features.TaskContext.Commands.DeleteTask;

/// <summary>
/// Validates the command to delete a task.
/// </summary>
public sealed class DeleteTaskCommandValidator 
    : AbstractValidator<DeleteTaskCommand>
{
    private readonly ITaskRepository _taskRepository;

    /// <summary>
    /// Initializes a new instance of the DeleteTaskCommandValidator.
    /// </summary>
    /// <param name="taskRepository">The task repository used for task validation.</param>
    public DeleteTaskCommandValidator(
        ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;

        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(MustBeInDatabase)
            .WithMessage(ErrorsResource.NotFound);
    }

    /// <summary>
    /// Checks if the task with the given ID exists in the database.
    /// </summary>
    /// <param name="command">The delete task command.</param>
    /// <param name="id">The ID of the task to be deleted.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>True if the task exists in the database, otherwise false.</returns>
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
