using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.EmployeeProjectsContext.Dtos;

namespace PM.Application.Features.EmployeeProjectsContext.Commands.RemoveEmployeeFromProject;

/// <summary>
/// Handles the execution of the <see cref="RemoveUserFromProjectCommand"/>.
/// </summary>
internal sealed class RemoveUserFromProjectCommandHandler
    : IRequestHandler<RemoveUserFromProjectCommand, ErrorOr<RemoveUserFromProjectResult>>
{
    private readonly IProjectRepository _projectRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="RemoveUserFromProjectCommandHandler"/> class.
    /// </summary>
    /// <param name="projectRepository">The project repository used for removing an employee 
    /// from a project.</param>
    public RemoveUserFromProjectCommandHandler(
        IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    /// <summary>
    /// Handles the removal of an employee from a project as specified in the command.
    /// </summary>
    /// <param name="command">The command containing information about the employee and project.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>An <see cref="ErrorOr{RemoveEmployeeFromProjectResult}"/> representing the 
    /// result of the operation.</returns>
    public async Task<ErrorOr<RemoveUserFromProjectResult>> Handle(
        RemoveUserFromProjectCommand command,
        CancellationToken cancellationToken)
    {
        command.Project!.RemoveEmployee(command.User!);

        await _projectRepository.SaveChangesAsync(cancellationToken);

        return new RemoveUserFromProjectResult(command.UserId);
    }
}