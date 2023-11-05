using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Resources;
using PM.Application.Features.TaskContext.Dtos;
using PM.Domain.Common.Extensions;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Features.TaskContext.Commands.CreateTask;

/// <summary>
/// Handles the command to create a new task.
/// </summary>
internal sealed class CreateTaskCommandHandler
    : IRequestHandler<CreateTaskCommand, ErrorOr<CreateTaskResult>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUser;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateTaskCommandHandler"/> class.
    /// </summary>
    /// <param name="taskRepository">The task repository.</param>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="currentUser">The current user service.</param>
    public CreateTaskCommandHandler(
        ITaskRepository taskRepository, 
        IUserRepository userRepository,
        ICurrentUserService currentUser)
    {
        _currentUser = currentUser;
        _taskRepository = taskRepository;
        _userRepository = userRepository;

    }

    /// <summary>
    /// Handles the create task command.
    /// </summary>
    /// <param name="command">The create task command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An error result or a result containing the task creation result.</returns>
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
