using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.EmployeeContext.Dtos;

namespace PM.Application.Features.UserContext.Commands.DeleteUser;

/// <summary>
/// Handles the command to delete a user.
/// </summary>
public sealed class DeleteUserCommandHandler
    : IRequestHandler<DeleteUserCommand, ErrorOr<DeleteUserResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly ITaskRepository _taskRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteUserCommandHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The repository for users.</param>
    /// <param name="taskRepository">The repository for tasks.</param>
    public DeleteUserCommandHandler(
        IUserRepository userRepository,
        ITaskRepository taskRepository)
    {
        _userRepository = userRepository;
        _taskRepository = taskRepository;

    }

    /// <summary>
    /// Handles the user deletion command.
    /// </summary>
    /// <param name="command">The delete user command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A result indicating the outcome of the deletion operation.</returns>
    public async Task<ErrorOr<DeleteUserResult>> Handle(
        DeleteUserCommand command,
        CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository
            .GetTaskIncludeAuthorByAuthorIdAsync(command.UserId, cancellationToken);

        tasks.ForEach(t => t.RemoveAuthor());

        await _userRepository
            .RemoveAsync(command.User!, cancellationToken);

        return new DeleteUserResult(command.UserId);
    }
}
