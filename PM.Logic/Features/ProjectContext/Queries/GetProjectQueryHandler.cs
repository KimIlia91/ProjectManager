using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.ProjectContext.Dtos;

namespace PM.Application.Features.ProjectContext.Queries;

public sealed class GetProjectQueryHandler
    : IRequestHandler<GetProjectQuery, ErrorOr<GetProjectResult>>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectQueryHandler(
        IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public Task<ErrorOr<GetProjectResult>> Handle(
        GetProjectQuery query, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
