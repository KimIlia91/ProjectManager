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
        var employee = await _employeeRepository
            .GetOrDeafaultAsync(e => e.Id == command.EmployeeId, cancellationToken);

        if (employee is null)
            return Error.NotFound("Not found", nameof(command.EmployeeId));

        await _employeeRepository.RemoveAsync(employee, cancellationToken);
        return new DeleteEmployeeResult();
    }
}
