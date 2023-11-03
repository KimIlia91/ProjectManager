using ErrorOr;
using MediatR;
using PM.Application.Common.Extensions;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.ProjectContext.Dtos;

namespace PM.Application.Features.ProjectContext.Queries.GetUserProjectList;

/// <summary>
/// Represents a handler for the GetUserProjectListQuery, responsible for retrieving a 
/// list of projects for a user.
/// </summary>
internal sealed class GetUserProjectListQueryHandler
    : IRequestHandler<GetUserProjectListQuery, ErrorOr<List<GetProjectListResult>>>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IProjectRepository _projectRepository;

    /// <summary>
    /// Initializes a new instance of the GetUserProjectListQueryHandler class.
    /// </summary>
    /// <param name="currentUser">An instance of the ICurrentUserService for obtaining 
    /// the current user's information.</param>
    /// <param name="projectRepository">An instance of the IProjectRepository for 
    /// interacting with project data.</param>
    public GetUserProjectListQueryHandler(
        ICurrentUserService currentUser,
        IProjectRepository projectRepository)
    {
        _currentUser = currentUser;
        _projectRepository = projectRepository;
    }

    /// <summary>
    /// Handles the GetUserProjectListQuery to retrieve a list of projects for the current user.
    /// </summary>
    /// <param name="query">The GetUserProjectListQuery containing filter and sorting criteria.</param>
    /// <param name="cancellationToken">A CancellationToken for handling asynchronous operations.</param>
    /// <returns>A list of projects that match the specified criteria or an error if the operation fails.</returns>
    public async Task<ErrorOr<List<GetProjectListResult>>> Handle(
        GetUserProjectListQuery query,
        CancellationToken cancellationToken)
    {
        var projectQuery = _projectRepository
            .GetQuiery(asNoTracking: true)
            .Where(p => p.Manager.Id == _currentUser.UserId ||
                        p.Employees.Any(e => e.Id == _currentUser.UserId))
            .Filter(query.Filetr)
            .Sort(query.SotrBy);

        return await _projectRepository
            .ToListResultAsync<GetProjectListResult>(projectQuery, cancellationToken);
    }
}
