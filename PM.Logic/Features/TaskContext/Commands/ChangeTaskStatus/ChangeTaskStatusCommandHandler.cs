using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.TaskContext.Dtos;

namespace PM.Application.Features.TaskContext.Commands.ChangeTaskStatus;

internal sealed class ChangeTaskStatusCommandHandler
    : IRequestHandler<ChangeTaskStatusCommand, ErrorOr<ChangeTaskStatusResult>>
{
    private readonly ITaskRepository _taskRepository;

    public ChangeTaskStatusCommandHandler(
        ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<ErrorOr<ChangeTaskStatusResult>> Handle(
        ChangeTaskStatusCommand command, 
        CancellationToken cancellationToken)
    {
        command.Task!.ChangeStatus(command.Status);

        await _taskRepository.SaveChangesAsync(cancellationToken);

        return new ChangeTaskStatusResult(command.Task.Id);
    }
}