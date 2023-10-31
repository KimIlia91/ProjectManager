using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.TaskContext.Dtos;

namespace PM.Application.Features.TaskContext.Commands.DeleteTask;

internal sealed class DeleteTaskCommandHandler
    : IRequestHandler<DeleteTaskCommand, ErrorOr<DeleteTaskResult>>
{
    private readonly ITaskRepository _taskRepository;

    public DeleteTaskCommandHandler(
        ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<ErrorOr<DeleteTaskResult>> Handle(
        DeleteTaskCommand command, 
        CancellationToken cancellationToken)
    {
        var task = await _taskRepository
            .GetOrDeafaultAsync(t => t.Id == command.Id, cancellationToken);

        if (task is null)
            return Error.NotFound("Not found", nameof(command.Id));

        await _taskRepository.RemoveAsync(task, cancellationToken);

        return new DeleteTaskResult();
    }
}
