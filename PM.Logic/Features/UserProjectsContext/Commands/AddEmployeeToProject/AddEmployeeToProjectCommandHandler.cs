using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.EmployeeProjectsContext.Dtos;

namespace PM.Application.Features.EmployeeProjectsContext.Commands.AddEmployeeToProject;

/// <summary>
/// Handles the command to add an employee to a project.
/// </summary>
internal sealed class AddEmployeeToProjectCommandHandler
    : IRequestHandler<AddEmployeeToProjectCommand, ErrorOr<AddEmployeeToProjectResult>>
{
    private readonly IProjectRepository _projectRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddEmployeeToProjectCommandHandler"/> class.
    /// </summary>
    /// <param name="projectRepository">The project repository used for database operations.</param>
    public AddEmployeeToProjectCommandHandler(
        IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    /// <summary>
    /// Handles the command to add an employee to a project.
    /// </summary>
    /// <param name="command">The command to add an employee to a project.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An error result or a result indicating the addition of an employee to the project.</returns>
    public async Task<ErrorOr<AddEmployeeToProjectResult>> Handle(
        AddEmployeeToProjectCommand command,
        CancellationToken cancellationToken)
    {
        command.Project!.AddEmployee(command.Employee!);
        await _projectRepository.SaveChangesAsync(cancellationToken);

        return new AddEmployeeToProjectResult(command.EmployeeId);
    }
}
