using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Resources;
using PM.Application.Features.EmployeeContext.Dtos;

namespace PM.Application.Features.EmployeeContext.Queries.GetEmployee;

/// <summary>
/// Handles the retrieval of user information based on a specific user query.
/// </summary>
public sealed class GetUserQueryHandler
    : IRequestHandler<GetUserQuery, ErrorOr<GetUserResult>>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserQueryHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The repository for user data.</param>
    public GetUserQueryHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Handles the user query and returns information about the requested user.
    /// </summary>
    /// <param name="query">The query specifying the user to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An <see cref="ErrorOr{T}"/> containing user information or an error if the user is not found.</returns>
    public async Task<ErrorOr<GetUserResult>> Handle(
        GetUserQuery query,
        CancellationToken cancellationToken)
    {
        var employee = await _userRepository
            .GetUserResultByIdAsync(query.UserId, cancellationToken);

        if (employee is null)
            return Error.NotFound(ErrorsResource.NotFound, nameof(query.UserId));

        return employee;
    }
}