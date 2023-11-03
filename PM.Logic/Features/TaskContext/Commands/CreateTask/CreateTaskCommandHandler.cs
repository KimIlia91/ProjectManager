using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Resources;
using PM.Application.Features.TaskContext.Dtos;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Features.TaskContext.Commands.CreateTask;

internal sealed class CreateTaskCommandHandler
    : IRequestHandler<CreateTaskCommand, ErrorOr<CreateTaskResult>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUser;

    public CreateTaskCommandHandler(
        ITaskRepository taskRepository, 
        IUserRepository userRepository,
        ICurrentUserService currentUser)
    {
        _currentUser = currentUser;
        _taskRepository = taskRepository;
        _userRepository = userRepository;

    }

    public async Task<ErrorOr<CreateTaskResult>> Handle(
        CreateTaskCommand command, 
        CancellationToken cancellationToken)
    {
        var author = await _userRepository
            .GetOrDeafaultAsync(u => u.Id == _currentUser.UserId, cancellationToken);

        if (author is null)
            return Error.Conflict(ErrorsResource.NotFound, nameof(author));

        var result = Task.Create(
            command.Name,
            author, 
            command.Executor, 
            command.Project!,
            command.Comment!,
            command.Status,
            command.Priority);

        if (result.IsError)
            return result.Errors;

        await _taskRepository.AddAsync(result.Value, cancellationToken);
        return new CreateTaskResult(result.Value.Id);
    }
}
