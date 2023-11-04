using ErrorOr;
using MediatR;
using PM.Application.Features.TaskContext.Dtos;
using PM.Domain.Common.Enums;
using System.Text.Json.Serialization;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Features.TaskContext.Commands.ChangeTaskStatus;

/// <summary>
/// Represents a command to change the status of a task.
/// </summary>
public sealed class ChangeTaskStatusCommand : IRequest<ErrorOr<ChangeTaskStatusResult>>
{
    /// <summary>
    /// Gets or sets the task associated with the command.
    /// </summary>
    [JsonIgnore] public Task? Task { get; set; }

    /// <summary>
    /// Gets or sets the ID of the task to be modified.
    /// </summary>
    public int TaskId { get; set; }

    /// <summary>
    /// Gets or sets the new status for the task.
    /// </summary>
    public Status Status { get; set; }
}
