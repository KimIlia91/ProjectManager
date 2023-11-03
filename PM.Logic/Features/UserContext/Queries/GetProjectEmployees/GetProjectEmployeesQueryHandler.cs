using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Models.Employee;

namespace PM.Application.Features.EmployeeContext.Queries.GetProjectEmployees;

internal sealed class GetProjectEmployeesQueryHandler
    : IRequestHandler<GetProjectEmployeesQuery, ErrorOr<List<UserResult>>>
{
    private readonly IUserRepository _employeeRepository;

    public GetProjectEmployeesQueryHandler(
        IUserRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<ErrorOr<List<UserResult>>> Handle(
        GetProjectEmployeesQuery query,
        CancellationToken cancellationToken)
    {
        var employeeQuery = _employeeRepository
            .GetQuiery()
            .Where(p => p.Projects.Any(e => e.Id == query.ProjctId));

        return await _employeeRepository
            .ToListResultAsync<UserResult>(employeeQuery, cancellationToken);
    }
}
