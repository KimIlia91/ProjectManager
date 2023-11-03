using ErrorOr;
using MediatR;
using PM.Application.Common.Extensions;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.ProjectContext.Dtos;

namespace PM.Application.Features.ProjectContext.Queries.GetUserProjectList;

internal sealed class GetUserProjectListQueryHandler
    : IRequestHandler<GetUserProjectListQuery, ErrorOr<List<GetProjectListResult>>>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IProjectRepository _projectRepository;

    public GetUserProjectListQueryHandler(
        ICurrentUserService currentUser,
        IProjectRepository projectRepository)
    {
        _currentUser = currentUser;
        _projectRepository = projectRepository;
    }

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
