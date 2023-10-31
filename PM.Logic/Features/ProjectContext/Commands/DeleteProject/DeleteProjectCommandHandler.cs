using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.ProjectContext.Dtos;

namespace PM.Application.Features.ProjectContext.Commands.DeleteProject;

public sealed class DeleteProjectCommandHandler
    : IRequestHandler<DeleteProjectCommand, ErrorOr<DeleteProjectResult>>
{
    private readonly IProjectRepository _projectRepository;

    public DeleteProjectCommandHandler(
        IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ErrorOr<DeleteProjectResult>> Handle(
        DeleteProjectCommand command,
        CancellationToken cancellationToken)
    {
        await _projectRepository.RemoveAsync(command.Project, cancellationToken);

        return new DeleteProjectResult(command.Id);
    }
}
