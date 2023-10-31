using ErrorOr;
using MediatR;
using PM.Application.Features.ProjectContext.Dtos;
using PM.Domain.Common.Enums;
using PM.Domain.Entities;
using System.Text.Json.Serialization;

namespace PM.Application.Features.ProjectContext.Commands.CreateProject;

public sealed class CreateProjectCommand : IRequest<ErrorOr<CreateProjectResult>>
{
    [JsonIgnore] public Company CustomerCompany { get; set; } = null!;

    [JsonIgnore] public Company ExecutorCompany { get; set; } = null!;

    [JsonIgnore] public Employee Manager { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int CustomerCompanyId { get; set; }

    public int ExecutorCompanyId { get; set; }

    public int ManagerId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public ProjectPriority Priority { get; set; }
}