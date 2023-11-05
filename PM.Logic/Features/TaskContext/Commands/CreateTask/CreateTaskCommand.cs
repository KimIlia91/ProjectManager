using ErrorOr;
using MediatR;
using PM.Application.Features.TaskContext.Dtos;
using PM.Domain.Common.Enums;
using PM.Domain.Entities;
using System.Text.Json.Serialization;

namespace PM.Application.Features.TaskContext.Commands.CreateTask;

/// <summary>
/// Represents a command to create a new task.
/// </summary>
public sealed class CreateTaskCommand
    : IRequest<ErrorOr<CreateTaskResult>>
{
    /// <summary>
    /// Gets or sets the executor for the task.
    /// </summary>
    [JsonIgnore] public User? Executor { get; set; }

    /// <summary>
    /// Gets or sets the project associated with the task.
    /// </summary>
    [JsonIgnore] public Project? Project { get; set; }

    /// <summary>
    /// Gets or sets the ID of the project.
    /// </summary>
    public int ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the name of the task.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the ID of the executor.
    /// </summary>
    public int ExecutorId { get; set; }

    /// <summary>
    /// Gets or sets an optional comment for the task.
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