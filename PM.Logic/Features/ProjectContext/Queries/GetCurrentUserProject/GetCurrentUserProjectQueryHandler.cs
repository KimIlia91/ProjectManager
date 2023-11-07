using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Resources;
using PM.Application.Features.ProjectContext.Dtos;

namespace PM.Application.Features.ProjectContext.Queries.GetCurrentUserProject;

internal sealed class GetCurrentUserProjectQueryHandler
    : IRequestHandler<GetCurrentUserProjectQuery, ErrorOr<GetProjectResult>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly ICurrentUserService _currentUser;

    public GetCurrentUserProjectQueryHandler(
        IProjectRepository projectRepository,
        ICurrentUserService currentUserService)
    {
        _projectRepository = projectRepository;
        _currentUser = currentUserService;
    }

    public async Task<ErrorOr<GetProjectResult>> Handle(
        GetCurrentUserProjectQuery query,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository
            .GetProjectOfUserAsync(query.ProjectId, _currentUser.UserId, cancellationToken);

        if (project is null)
            return Error.NotFound(ErrorsResource.NotFound, nameof(query.ProjectId));

        return project;
    }
}
