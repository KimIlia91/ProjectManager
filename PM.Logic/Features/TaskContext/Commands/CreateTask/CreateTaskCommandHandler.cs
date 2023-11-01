using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.TaskContext.Dtos;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Features.TaskContext.Commands.CreateTask;

internal sealed class CreateTaskCommandHandler
    : IRequestHandler<CreateTaskCommand, ErrorOr<CreateTaskResult>>
{
    private readonly ITaskRepository _taskRepository;

    public CreateTaskCommandHandler(
        ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<ErrorOr<CreateTaskResult>> Handle(
        CreateTaskCommand command, 
        CancellationToken cancellationToken)
    {
        var result = Task.Create(
            command.Name, 
            command.Author, 
            command.Executor, 
            command.Project!,
            command.Commnet!,
            command.Status,
            command.Priority);

        if (result.IsError)
            return result.Errors;

        await _taskRepository.AddAsync(result.Value, cancellationToken);
        return new CreateTaskResult(result.Value.Id);
    }
}
