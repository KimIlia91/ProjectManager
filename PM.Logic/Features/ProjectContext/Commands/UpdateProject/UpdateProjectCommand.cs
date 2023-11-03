using ErrorOr;
using MediatR;
using PM.Application.Features.ProjectContext.Dtos;
using PM.Domain.Common.Enums;
using PM.Domain.Entities;
using System.Text.Json.Serialization;

namespace PM.Application.Features.ProjectContext.Commands.UpdateProject;

/// <summary>
/// Represents a command for updating a project.
/// </summary>
public sealed class UpdateProjectCommand 
    : IRequest<ErrorOr<UpdateProjectResult>>
{
    [JsonIgnore] public Project Project { get; set; } = null!;

    [JsonIgnore] public User Manager { get; set; } = null!;

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string CustomerCompany { get; set; } = null!;

    public string ExecutorCompany { get; set; } = null!;

    public int ManagerId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public Priority Priority { get; set; }
}
