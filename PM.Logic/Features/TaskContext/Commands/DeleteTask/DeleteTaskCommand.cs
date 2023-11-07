using ErrorOr;
using MediatR;
using PM.Application.Features.TaskContext.Dtos;
using System.Text.Json.Serialization;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Features.TaskContext.Commands.DeleteTask;

/// <summary>
/// Represents a command to delete a task.
/// </summary>
public sealed class DeleteTaskCommand : IRequest<ErrorOr<DeleteTaskResult>>
{
    /// <summary>
    /// Gets or sets the task to be deleted.
    /// </summary>
    [JsonIgnore] public Task? Task { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the task to be deleted.
    /// </summary>
    public int TaskId { get; set; }
}