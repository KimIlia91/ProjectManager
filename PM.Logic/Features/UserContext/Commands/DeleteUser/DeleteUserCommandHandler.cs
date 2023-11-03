using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.EmployeeContext.Dtos;

namespace PM.Application.Features.UserContext.Commands.DeleteUser;

public sealed class DeleteUserCommandHandler
    : IRequestHandler<DeleteUserCommand, ErrorOr<DeleteUserResult>>
{
    private readonly IUserRepository _employeeRepository;
    private readonly ITaskRepository _taskRepository;

    public DeleteUserCommandHandler(
        IUserRepository employeeRepository,
        ITaskRepository taskRepository)
    {
        _employeeRepository = employeeRepository;
        _taskRepository = taskRepository;

    }

    public async Task<ErrorOr<DeleteUserResult>> Handle(
        DeleteUserCommand command,
        CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository
            .GetTaskByAuthorIdAsync(command.EmployeeId, cancellationToken);

        tasks.ForEach(t => t.RemoveAuthor());

        await _employeeRepository
            .RemoveAsync(command.Employee!, cancellationToken);

        return new DeleteUserResult();
    }
}
