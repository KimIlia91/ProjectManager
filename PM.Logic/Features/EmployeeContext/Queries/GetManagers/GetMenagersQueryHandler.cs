using ErrorOr;
using MediatR;
using PM.Application.Common.Enums;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Models.Employee;
using PM.Domain.Common.Extensions;

namespace PM.Application.Features.EmployeeContext.Queries.GetManagers;

internal sealed class GetMenagersQueryHandler
    : IRequestHandler<GetMenagersQuery, ErrorOr<List<EmployeeResult>>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetMenagersQueryHandler(
        IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    //TODO: Для получения всех менеджеров нужен котроллер метод
    public async Task<ErrorOr<List<EmployeeResult>>> Handle(
        GetMenagersQuery query, 
        CancellationToken cancellationToken)
    {
        var employee = _employeeRepository
            .GetQuiery(asNoTracking: true)
            .Where(e => e.EmployeeRoles
                .Any(er => er.Role.Name == RoleEnum.Manager.GetDescription()));

        return await _employeeRepository
            .ToListResultAsync<EmployeeResult>(employee, cancellationToken);
    }
}
