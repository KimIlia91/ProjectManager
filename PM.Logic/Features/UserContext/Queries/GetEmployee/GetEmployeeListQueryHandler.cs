using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Models.Employee;
using PM.Domain.Common.Enums;
using PM.Domain.Common.Extensions;

namespace PM.Application.Features.EmployeeContext.Queries.GetEmployee;
/// <summary>
/// Handles the request to retrieve a list of employee results.
/// </summary>
internal sealed class GetEmployeeListQueryHandler
    : IRequestHandler<GetEmployeeListQuery, ErrorOr<List<UserResult>>>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetEmployeeListQueryHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The repository for managing employees.</param>
    public GetEmployeeListQueryHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Handles the request to retrieve a list of employee results.
    /// </summary>
    /// <param name="query">The query to retrieve employees.</param>
    /// <param name="cancellationToken">The token to cancel the operation.</param>
    /// <returns>A list of user results representing employees.</returns>
    public async Task<ErrorOr<List<UserResult>>> Handle(
        GetEmployeeListQuery query,
        CancellationToken cancellationToken)
    {
        return await _userRepository
            .GetUserResultListByRoleAsync(RoleEnum.Employee.GetDescription(), cancellationToken);
    }
}
