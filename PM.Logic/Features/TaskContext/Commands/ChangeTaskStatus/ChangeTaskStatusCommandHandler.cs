using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.TaskContext.Dtos;

namespace PM.Application.Features.TaskContext.Commands.ChangeTaskStatus;

/// <summary>
/// Handles the execution of the ChangeTaskStatusCommand.
/// </summary>
internal sealed class ChangeTaskStatusCommandHandler
    : IRequestHandler<ChangeTaskStatusCommand, ErrorOr<ChangeTaskStatusResult>>
{
    private readonly ITaskRepository _taskRepository;

    /// <summary>
    /// Initializes a new instance of the ChangeTaskStatusCommandHandler class.
    /// </summary>
    /// <param name="taskRepository">The task repository to be used for data access.</param>
    public ChangeTaskStatusCommandHandler(
        ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    /// <summary>
    /// Handles the execution of the ChangeTaskStatusCommand.
    /// </summary>
    /// <param name="command">The ChangeTaskStatusCommand to be executed.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation, 
    /// returning the result of the execution.</returns>
    public async Task<ErrorOr<ChangeTaskStatusResult>> Handle(
        ChangeTaskStatusCommand command,
        CancellationToken cancellationToken)
    {
        command.Task!.ChangeStatus(command.Status);
        await _taskRepository.SaveChangesAsync(cancellationToken);
        return new ChangeTaskStatusResult(command.Task.Id);
    }
}