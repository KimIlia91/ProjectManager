using ErrorOr;
using MediatR;
using PM.Application.Features.ProjectContext.Dtos;
using PM.Domain.Entities;
using System.Text.Json.Serialization;

namespace PM.Application.Features.ProjectContext.Commands.DeleteProject;

public sealed class DeleteProjectCommand : IRequest<ErrorOr<DeleteProjectResult>>
{
    [JsonIgnore] public Project Project { get; set; }

    public int Id { get; set; }
}