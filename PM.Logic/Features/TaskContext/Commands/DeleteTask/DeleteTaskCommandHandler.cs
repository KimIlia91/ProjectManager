using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.TaskContext.Dtos;

namespace PM.Application.Features.TaskContext.Commands.DeleteTask;

/// <summary>
/// Handles the command to delete a task.
/// </summary>
internal sealed class DeleteTaskCommandHandler
    : IRequestHandler<DeleteTaskCommand, ErrorOr<DeleteTaskResult>>
{
    private readonly ITaskRepository _taskRepository;

    /// <summary>
    /// Initializes a new instance of the DeleteTaskCommandHandler.
    /// </summary>
    /// <param name="taskRepository">The task repository used for task deletion.</param>
    public DeleteTaskCommandHandler(
        ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    /// <summary>
    /// Handles the delete task command.
    /// </summary>
    /// <param name="command">The delete task command.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>An error result or success result after deleting the task.</returns>
    public async Task<ErrorOr<DeleteTaskResult>> Handle(
        DeleteTaskCommand command, 
        CancellationToken cancellationToken)
    {
        await _taskRepository.RemoveAsync(command.Task!, cancellationToken);

        return new DeleteTaskResult();
    }
}
