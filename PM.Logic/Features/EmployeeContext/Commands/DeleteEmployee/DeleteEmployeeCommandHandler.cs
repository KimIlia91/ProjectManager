using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.EmployeeContext.Dtos;

namespace PM.Application.Features.EmployeeContext.Commands.DeleteEmployee;

public sealed class DeleteEmployeeCommandHandler
    : IRequestHandler<DeleteEmployeeCommand, ErrorOr<DeleteEmployeeResult>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public DeleteEmployeeCommandHandler(
        IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<ErrorOr<DeleteEmployeeResult>> Handle(
        DeleteEmployeeCommand command, 
        CancellationToken cancellationToken)
    {
        await _employeeRepository
            .RemoveAsync(command.Employee!, cancellationToken);

        return new DeleteEmployeeResult();
    }
}
