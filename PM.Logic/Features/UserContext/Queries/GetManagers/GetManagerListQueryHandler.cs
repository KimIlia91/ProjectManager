using ErrorOr;
using MediatR;
using PM.Application.Common.Enums;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Models.Employee;
using PM.Domain.Common.Enums;
using PM.Domain.Common.Extensions;

namespace PM.Application.Features.EmployeeContext.Queries.GetManagers;

/// <summary>
/// Handler for processing the <see cref="GetManagerListQuery"/> to retrieve a list of manager results.
/// </summary>
internal sealed class GetManagerListQueryHandler
    : IRequestHandler<GetManagerListQuery, ErrorOr<List<UserResult>>>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetManagerListQueryHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The repository for accessing user data.</param>
    public GetManagerListQueryHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Handles the <see cref="GetManagerListQuery"/> to retrieve a list of manager results.
    /// </summary>
    /// <param name="query">The query to retrieve manager results.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation 
    /// and contains the list of manager results.</returns>
    public async Task<ErrorOr<List<UserResult>>> Handle(
        GetManagerListQuery query,
        CancellationToken cancellationToken)
    {
        return await _userRepository
            .GetUserResultListByRoleAsync(RoleEnum.Manager.GetDescription(), cancellationToken);
    }
}
