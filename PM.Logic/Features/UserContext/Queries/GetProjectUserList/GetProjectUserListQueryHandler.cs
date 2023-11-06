using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Models.Employee;

namespace PM.Application.Features.UserContext.Queries.GetProjectUserList;

/// <summary>
/// Handles the query to retrieve a list of employees associated with a project.
/// </summary>
internal sealed class GetProjectUserListQueryHandler
    : IRequestHandler<GetProjectUserListQuery, ErrorOr<List<UserResult>>>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProjectUserListQueryHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The repository for user-related operations.</param>
    public GetProjectUserListQueryHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Handles the <see cref="GetProjectUserListQuery"/> to retrieve a list of users associated with a project.
    /// </summary>
    /// <param name="query">The query parameters.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of user results associated with the specified project.</returns>
    public async Task<ErrorOr<List<UserResult>>> Handle(
        GetProjectUserListQuery query,
        CancellationToken cancellationToken)
    {
        return await _userRepository
            .GetUsersResultListByProjectIdAsync(query.ProjectId, cancellationToken);
    }
}

