using FluentValidation;
using PM.Application.Common.Resources;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Specifications.TaskSpecifications.Manager;

namespace PM.Application.Features.TaskContext.Commands.DeleteTask;

/// <summary>
/// Validates the command to delete a task.
/// </summary>
public sealed class DeleteTaskCommandValidator
    : AbstractValidator<DeleteTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ICurrentUserService _currentUserService;

    /// <summary>
    /// Initializes a new instance of the DeleteTaskCommandValidator.
    /// </summary>
    /// <param name="taskRepository">The task repository used for task validation.</param>
    /// <param name="currentUserService">The current user service</param>
    public DeleteTaskCommandValidator(
        ITaskRepository taskRepository,
        ICurrentUserService currentUserService)
    {
        _taskRepository = taskRepository;
        _currentUserService = currentUserService;

        RuleFor(command => command.TaskId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(MustBeInDatabase)
            .WithMessage(ErrorsResource.NotFound);
    }

    /// <summary>
    /// Checks if the task with the given ID exists in the database.
    /// </summary>
    /// <param name="command">The delete task command.</param>
    /// <param name="taskId">The ID of the task to be deleted.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>True if the task exists in the database, otherwise false.</returns>
    private async Task<bool> MustBeInDatabase(
        DeleteTaskCommand command,
        int taskId,
        CancellationToken cancellationToken)
    {
        var getTaskByManager = new TaskOfManagerSpec(taskId, _currentUserService);

        command.Task = await _taskRepository
             .GetOrDeafaultAsync(getTaskByManager.ToExpression(), cancellationToken);

        return command.Task is not null;
    }
}
