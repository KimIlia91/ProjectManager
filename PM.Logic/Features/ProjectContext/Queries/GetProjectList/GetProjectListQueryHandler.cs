using MediatR;
using PM.Application.Common.Extensions;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.ProjectContext.Dtos;

namespace PM.Application.Features.ProjectContext.Queries.GetProjectList;

internal sealed class GetProjectListQueryHandler
    : IRequestHandler<GetProjectListQuery, List<GetProjectListResult>>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectListQueryHandler(
        IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<List<GetProjectListResult>> Handle(
        GetProjectListQuery query,
        CancellationToken cancellationToken)
    {
        var projectQuery = _projectRepository
            .GetProjectQuiery(asNoTracking: true)
            .Where(query.Filter)
            .SortProject(query.SortBy);

        return await _projectRepository
            .ToProjectListResultAsync(projectQuery, cancellationToken);
    }
}