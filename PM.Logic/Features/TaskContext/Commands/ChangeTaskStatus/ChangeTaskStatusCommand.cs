using ErrorOr;
using MediatR;
using PM.Application.Features.TaskContext.Dtos;
using System.Text.Json.Serialization;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Features.TaskContext.Commands.ChangeTaskStatus;

public sealed class ChangeTaskStatusCommand : IRequest<ErrorOr<ChangeTaskStatusResult>>
{
    [JsonIgnore] public Task? Task { get; set; }

    public int TaskId { get; set; }

    public string Status { get; set; } = null!;
}