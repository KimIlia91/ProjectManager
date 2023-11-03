using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.TaskContext.Dtos;

namespace PM.Application.Features.TaskContext.Commands.DeleteTask;

/// <summary>
/// 
/// </summary>
internal sealed class DeleteTaskCommandHandler
    : IRequestHandler<DeleteTaskCommand, ErrorOr<DeleteTaskResult>>
{
    private readonly ITaskRepository _taskRepository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="taskRepository"></param>
    public DeleteTaskCommandHandler(
        ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ErrorOr<DeleteTaskResult>> Handle(
        DeleteTaskCommand command, 
        CancellationToken cancellationToken)
    {
        await _taskRepository.RemoveAsync(command.Task!, cancellationToken);

        return new DeleteTaskResult();
    }
}
