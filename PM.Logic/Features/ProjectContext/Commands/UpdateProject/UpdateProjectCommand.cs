using ErrorOr;
using MediatR;
using PM.Application.Features.ProjectContext.Dtos;
using PM.Domain.Common.Enums;
using PM.Domain.Entities;
using System.Text.Json.Serialization;

namespace PM.Application.Features.ProjectContext.Commands.UpdateProject;

public sealed class UpdateProjectCommand : IRequest<ErrorOr<UpdateProjectResult>>
{
    [JsonIgnore] public Project Project { get; set; } = null!;

    [JsonIgnore] public Company CustomerCompany { get; set; } = null!;

    [JsonIgnore] public Company ExecutorCompany { get; set; } = null!;

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CustomerCompanyId { get; set; }

    public int ExecutorCompanyId { get; set; }

    public int ManagerId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public ProjectPriority Priority { get; set; }
}
