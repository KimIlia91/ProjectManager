using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Models.Employee;

namespace PM.Application.Features.EmployeeContext.Queries.GetProjectEmployees;

internal sealed class GetProjectEmployeesQueryHandler
    : IRequestHandler<GetProjectEmployeesQuery, ErrorOr<List<UserResult>>>
{
    private readonly IUserRepository _userRepository;

    public GetProjectEmployeesQueryHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<List<UserResult>>> Handle(
        GetProjectEmployeesQuery query,
        CancellationToken cancellationToken)
    {
        return await _userRepository
            .GetProjectUserResultListAsync(query.ProjctId, cancellationToken);
    }
}
