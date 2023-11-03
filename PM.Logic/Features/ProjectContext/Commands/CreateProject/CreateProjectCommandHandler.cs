using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.ProjectContext.Dtos;
using PM.Domain.Entities;

namespace PM.Application.Features.ProjectContext.Commands.CreateProject;

/// <summary>
/// Handles the command for creating a new project.
/// </summary>
public sealed class CreateProjectCommandHandler
    : IRequestHandler<CreateProjectCommand, ErrorOr<CreateProjectResult>>
{
    private readonly IProjectRepository _projectRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProjectCommandHandler"/> class.
    /// </summary>
    /// <param name="projectRepository">The project repository used for data access.</param>
    public CreateProjectCommandHandler(
        IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    /// <summary>
    /// Handles the creation of a new project based on the provided command.
    /// </summary>
    /// <param name="command">The command containing project creation information.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An <see cref="ErrorOr{T}"/> result containing either the created project result or errors.</returns>
    public async Task<ErrorOr<CreateProjectResult>> Handle(
        CreateProjectCommand command, 
        CancellationToken cancellationToken)
    {
        var result = Project.Create(
            command.Name, 
            command.CustomerCompany, 
            command.ExecutorCompany, 
            command.Manager!, 
            command.StartDate, 
            command.EndDate,
            command.Priority);

        if (result.IsError)
            return result.Errors;

        await _projectRepository.AddAsync(result.Value, cancellationToken);
        return new CreateProjectResult(result.Value.Id);
    }
}