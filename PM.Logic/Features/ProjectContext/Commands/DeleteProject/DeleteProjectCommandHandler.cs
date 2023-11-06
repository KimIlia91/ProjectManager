using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.ProjectContext.Dtos;

namespace PM.Application.Features.ProjectContext.Commands.DeleteProject;

/// <summary>
/// Represents a command handler for deleting a project.
/// </summary>
internal sealed class DeleteProjectCommandHandler
    : IRequestHandler<DeleteProjectCommand, ErrorOr<DeleteProjectResult>>
{
    private readonly IProjectRepository _projectRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProjectCommandHandler"/> class.
    /// </summary>
    /// <param name="projectRepository">The repository for managing projects.</param>
    public DeleteProjectCommandHandler(
        IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    /// <summary>
    /// Handles the command to delete a project.
    /// </summary>
    /// <param name="command">The delete project command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An asynchronous operation that returns the result of the delete operation.</returns>
    public async Task<ErrorOr<DeleteProjectResult>> Handle(
        DeleteProjectCommand command,
        CancellationToken cancellationToken)
    {
        await _projectRepository.RemoveAsync(command.Project!, cancellationToken);

        return new DeleteProjectResult(command.Id);
    }
}
