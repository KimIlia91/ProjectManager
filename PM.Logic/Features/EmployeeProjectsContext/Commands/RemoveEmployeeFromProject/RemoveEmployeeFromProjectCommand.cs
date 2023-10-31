using ErrorOr;
using MediatR;
using PM.Application.Features.EmployeeProjectsContext.Dtos;
using PM.Domain.Entities;
using System.Text.Json.Serialization;

namespace PM.Application.Features.EmployeeProjectsContext.Commands.RemoveEmployeeFromProject;

public sealed class RemoveEmployeeFromProjectCommand
    : IRequest<ErrorOr<RemoveEmployeeFromProjectResult>>
{
    [JsonIgnore] public Project? Project { get; set; }

    [JsonIgnore] public Employee? Employee { get; set; }

    public int EmployeeId { get; set; }

    public int ProjectId { get; set; }
}