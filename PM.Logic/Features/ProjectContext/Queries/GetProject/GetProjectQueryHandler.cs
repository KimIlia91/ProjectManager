using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Resources;
using PM.Application.Features.ProjectContext.Dtos;

namespace PM.Application.Features.ProjectContext.Queries.GetProject;

/// <summary>
/// Represents a query to retrieve project information by its ID.
/// </summary>
public sealed class GetProjectQueryHandler
    : IRequestHandler<GetProjectQuery, ErrorOr<GetProjectResult>>
{
    private readonly IProjectRepository _projectRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProjectQueryHandler"/> class.
    /// </summary>
    /// <param name="projectRepository">The project repository to retrieve project data.</param>
    public GetProjectQueryHandler(
        IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    /// <summary>
    /// Handles the processing of a query to retrieve project information by its ID.
    /// </summary>
    /// <param name="query">The query containing the project ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An <see cref="ErrorOr{T}"/> representing the result of the query.</returns>
    public async Task<ErrorOr<GetProjectResult>> Handle(
        GetProjectQuery query,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository
            .GetProjectResultByIdAsync(query.Id, cancellationToken);

        if (project is null)
            return Error.NotFound(ErrorsResource.NotFound, nameof(query.Id));

        return project;
    }
}
