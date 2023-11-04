using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.EmployeeContext.Dtos;

namespace PM.Application.Features.EmployeeContext.Queries.GetEmployees;

/// <summary>
/// Handles the retrieval of a list of user information based on the specified query.
/// </summary>
internal sealed class GetUserListQueryHandler 
    : IRequestHandler<GetUserListQuery, ErrorOr<List<GetUserResult>>>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the GetUsersQueryHandler class.
    /// </summary>
    /// <param name="userRepository">The user repository to fetch user information from.</param>
    public GetUserListQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Handles the query to retrieve a list of user information.
    /// </summary>
    /// <param name="query">The query specifying the retrieval of user information.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A list of user information wrapped in an ErrorOr result.</returns>
    public async Task<ErrorOr<List<GetUserResult>>> Handle(
        GetUserListQuery query, 
        CancellationToken cancellationToken)
    {
        return await _userRepository
            .GetUserResultListAsync(cancellationToken);
    }
}

