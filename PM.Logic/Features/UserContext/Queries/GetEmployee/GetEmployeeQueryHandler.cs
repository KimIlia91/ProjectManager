using ErrorOr;
using MediatR;
using PM.Application.Common.Enums;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Models.Employee;
using PM.Domain.Common.Extensions;

namespace PM.Application.Features.EmployeeContext.Queries.GetEmployee;

internal sealed class GetEmployeeQueryHandler
    : IRequestHandler<GetEmployeeQuery, ErrorOr<List<UserResult>>>
{
    private readonly IUserRepository _employeeRepository;

    public GetEmployeeQueryHandler(
        IUserRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<ErrorOr<List<UserResult>>> Handle(
        GetEmployeeQuery query, 
        CancellationToken cancellationToken)
    {
        return await _employeeRepository
            .GetUserResultListByRoleAsync(RoleEnum.Employee.GetDescription(), cancellationToken);
    }
}
