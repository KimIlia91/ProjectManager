using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.EmployeeContext.Dtos;

namespace PM.Application.Features.EmployeeContext.Queries.GetEmployees;

public sealed class GetEmployeesQueryHandler
    : IRequestHandler<GetEmployeesQuery, ErrorOr<List<GetEmployeeResult>>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeesQueryHandler(
        IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<ErrorOr<List<GetEmployeeResult>>> Handle(
        GetEmployeesQuery query, 
        CancellationToken cancellationToken)
    {
        return await _employeeRepository.GetEmployeesAsync(cancellationToken);
    }
}
