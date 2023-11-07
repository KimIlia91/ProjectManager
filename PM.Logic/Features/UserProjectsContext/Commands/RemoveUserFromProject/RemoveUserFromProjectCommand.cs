using ErrorOr;
using MediatR;
using PM.Application.Features.EmployeeProjectsContext.Dtos;
using PM.Domain.Entities;
using System.Text.Json.Serialization;

namespace PM.Application.Features.EmployeeProjectsContext.Commands.RemoveEmployeeFromProject;

/// <summary>
/// Command to remove an employee from a project.
/// </summary>
public sealed class RemoveUserFromProjectCommand
    : IRequest<ErrorOr<RemoveUserFromProjectResult>>
{
    /// <summary>
    /// Gets or sets the project from which the employee should be removed.
    /// </summary>
    [JsonIgnore]
    public Project? Project { get; set; }

    /// <summary>
    /// Gets or sets the employee to be removed from the project.
    /// </summary>
    [JsonIgnore]
    public User? User { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user to be removed from the project.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the project from which the employee should be removed.
    /// </summary>
    public int ProjectId { get; set; }
}
