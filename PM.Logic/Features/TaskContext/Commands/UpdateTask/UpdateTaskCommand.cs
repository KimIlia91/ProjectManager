using ErrorOr;
using MediatR;
using PM.Application.Features.TaskContext.Dtos;
using PM.Domain.Common.Enums;
using PM.Domain.Entities;
using System.Text.Json.Serialization;
using Task = PM.Domain.Entities.Task;
using Status = PM.Domain.Common.Enums.Status;

namespace PM.Application.Features.TaskContext.Commands.UpdateTask;

/// <summary>
/// Represents a command to update a task.
/// </summary>
public sealed class UpdateTaskCommand : IRequest<ErrorOr<UpdateTaskResult>>
{
    /// <summary>
    /// Gets or sets the task to be updated.
    /// </summary>
    [JsonIgnore] public Task? Task { get; set; }

    /// <summary>
    /// Gets or sets the author of the task.
    /// </summary>
    [JsonIgnore] public User? Author { get; set; }

    /// <summary>
    /// Gets or sets the executor of the task.
    /// </summary>
    [JsonIgnore] public User? Executor { get; set; }

    /// <summary>
    /// Gets or sets the ID of the task to be updated.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the task.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the ID of the author.
    /// </summary>
    public int AuthorId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the executor.
    /// </summary>
    public int ExecutorId { get; set; }

    /// <summary>
    /// Gets or sets the comment associated with the task.
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Gets or sets the status of the task.
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// Gets or sets the priority of the task.
    /// </summary>
    public Priority Priority { get; set; }
}
