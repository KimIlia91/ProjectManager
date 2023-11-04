using PM.Domain.Common.Enums;
using System;

namespace PM.Application.Common.Models.Task;

/// <summary>
/// Represents a filter for querying tasks.
/// </summary>
public sealed class TaskFilter
{
    /// <summary>
    /// Gets or sets the status of the tasks to filter by.
    /// </summary>
    public Status? Status { get; set; }

    /// <summary>
    /// Gets or sets the priority of the tasks to filter by.
    /// </summary>
    public Priority? Priority { get; set; }

    /// <summary>
    /// Gets or sets the ID of the task author to filter by.
    /// </summary>
    public int? AuthorId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the task executor to filter by.
    /// </summary>
    public int? ExecutorId { get; set; }
}