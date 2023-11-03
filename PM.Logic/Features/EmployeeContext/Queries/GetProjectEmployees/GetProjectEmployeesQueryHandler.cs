using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Models.Employee;

namespace PM.Application.Features.EmployeeContext.Queries.GetProjectEmployees;

internal sealed class GetProjectEmployeesQueryHandler
    : IRequestHandler<GetProjectEmployeesQuery, ErrorOr<List<EmployeeResult>>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetProjectEmployeesQueryHandler(
        IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<ErrorOr<List<EmployeeResult>>> Handle(
        GetProjectEmployeesQuery query,
        CancellationToken cancellationToken)
    {
        var employeeQuery = _employeeRepository
            .GetQuiery()
            .Where(p => p.Projects.Any(e => e.Id == query.ProjctId));

        return await _employeeRepository
            .ToListResultAsync<EmployeeResult>(employeeQuery, cancellationToken);
    }
}
