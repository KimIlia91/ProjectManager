using ErrorOr;
using MediatR;
using PM.Application.Common.Enums;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Models.Employee;
using PM.Domain.Common.Extensions;

namespace PM.Application.Features.EmployeeContext.Queries.GetManagers;

internal sealed class GetMenagersQueryHandler
    : IRequestHandler<GetMenagersQuery, ErrorOr<List<UserResult>>>
{
    private readonly IUserRepository _employeeRepository;

    public GetMenagersQueryHandler(
        IUserRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    //TODO: Для получения всех менеджеров нужен котроллер метод
    public async Task<ErrorOr<List<UserResult>>> Handle(
        GetMenagersQuery query, 
        CancellationToken cancellationToken)
    {
        return await _employeeRepository
            .GetUserResultListByRoleAsync(RoleEnum.Manager.GetDescription(), cancellationToken);
    }
}
