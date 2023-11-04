using ErrorOr;
using MediatR;
using PM.Application.Features.EmployeeProjectsContext.Dtos;
using PM.Domain.Entities;
using System.Text.Json.Serialization;

namespace PM.Application.Features.EmployeeProjectsContext.Commands.AddEmployeeToProject;

/// <summary>
/// Represents a command to add an employee to a project.
/// </summary>
public sealed class AddEmployeeToProjectCommand 
    : IRequest<ErrorOr<AddEmployeeToProjectResult>>
{
    /// <summary>
    /// Gets or sets the project to which the employee will be added.
    /// </summary>
    [JsonIgnore] public Project? Project { get; set; }

    /// <summary>
    /// Gets or sets the employee to be added to the project.
    /// </summary>
    [JsonIgnore] public User? Employee { get; set; }

    /// <summary>
    /// Gets or sets the ID of the employee to be added.
    /// </summary>
    public int EmployeeId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the project to which the employee will be added.
    /// </summary>
    public int ProjectId { get; set; }
}
