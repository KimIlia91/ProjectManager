using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.TaskContext.Dtos;

namespace PM.Application.Features.TaskContext.Commands.UpdateTask;

internal sealed class UpdateTaskCommandHandler
    : IRequestHandler<UpdateTaskCommand, ErrorOr<UpdateTaskResult>>
{
    private readonly ITaskRepository _taskRepository;

    public UpdateTaskCommandHandler(
        ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<ErrorOr<UpdateTaskResult>> Handle(
        UpdateTaskCommand command, 
        CancellationToken cancellationToken)
    {
        command.Task!.Update(
            command.Name,
            command.Author!,
            command.Executor!,
            command.Commnet!,
            command.Status,
            command.Priority);

        await _taskRepository.SaveChangesAsync(cancellationToken);
        return new UpdateTaskResult(command.Task.Id);
    }
}
