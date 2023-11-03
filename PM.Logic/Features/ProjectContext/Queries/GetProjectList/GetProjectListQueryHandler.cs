using ErrorOr;
using MediatR;
using PM.Application.Common.Extensions;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.ProjectContext.Dtos;

namespace PM.Application.Features.ProjectContext.Queries.GetProjectList;

/// <summary>
/// Handles the execution of a query to retrieve a list of project information.
/// </summary>
internal sealed class GetProjectListQueryHandler
    : IRequestHandler<GetProjectListQuery, ErrorOr<List<GetProjectListResult>>>
{
    private readonly IProjectRepository _projectRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProjectListQueryHandler"/> class.
    /// </summary>
    /// <param name="projectRepository">The repository for accessing project data.</param>
    public GetProjectListQueryHandler(
        IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    /// <summary>
    /// Handles the execution of the project list query.
    /// </summary>
    /// <param name="query">The query specifying filter criteria and sorting options.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A list of project information or an error result.</returns>
    public async Task<ErrorOr<List<GetProjectListResult>>> Handle(
        GetProjectListQuery query,
        CancellationToken cancellationToken)
    {
        var projectQuery = _projectRepository
            .GetQuiery(asNoTracking: true)
            .Filter(query.Filter)
            .Sort(query.SortBy);

        return await _projectRepository
            .ToListResultAsync<GetProjectListResult>(projectQuery, cancellationToken);
    }
}