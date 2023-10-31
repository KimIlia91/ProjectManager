using ErrorOr;
using MediatR;
using PM.Application.Features.ProjectContext.Dtos;
using PM.Domain.Entities;
using System.Text.Json.Serialization;

namespace PM.Application.Features.ProjectContext.Commands.AddEmployeeToProject;

public sealed class AddEmployeeToProjectCommand 
    : IRequest<ErrorOr<AddEmployeeToProjectResult>>
{
    [JsonIgnore] public Project? Project { get; set; }

    [JsonIgnore] public Employee? Employee { get; set; }

    public int EmployeeId { get; set; }

    public int ProjectId { get; set; }
}