using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.EmployeeContext.Dtos;

namespace PM.Application.Features.EmployeeContext.Commands.DeleteEmployee;

public sealed class DeleteEmployeeCommandHandler
    : IRequestHandler<DeleteEmployeeCommand, ErrorOr<DeleteEmployeeResult>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ITaskRepository _taskRepository;

    public DeleteEmployeeCommandHandler(
        IEmployeeRepository employeeRepository, 
        ITaskRepository taskRepository)
    {
        _employeeRepository = employeeRepository;
        _taskRepository = taskRepository;

    }

    public async Task<ErrorOr<DeleteEmployeeResult>> Handle(
        DeleteEmployeeCommand command, 
        CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository
            .GetTaskByAuthorIdAsync(command.EmployeeId, cancellationToken);

        tasks.ForEach(t => t.RemoveAuthor());

        await _employeeRepository
            .RemoveAsync(command.Employee!, cancellationToken);

        return new DeleteEmployeeResult();
    }
}
