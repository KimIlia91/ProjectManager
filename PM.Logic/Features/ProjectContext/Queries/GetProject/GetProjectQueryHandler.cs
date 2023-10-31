using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.ProjectContext.Dtos;

namespace PM.Application.Features.ProjectContext.Queries.GetProject;

public sealed class GetProjectQueryHandler
    : IRequestHandler<GetProjectQuery, ErrorOr<GetProjectResult>>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectQueryHandler(
        IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ErrorOr<GetProjectResult>> Handle(
        GetProjectQuery query,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository
            .GetProjectByIdAsync(query.Id, cancellationToken);

        if (project is null)
            return Error.NotFound("Not found", nameof(query.Id));

        return project;
    }
}
