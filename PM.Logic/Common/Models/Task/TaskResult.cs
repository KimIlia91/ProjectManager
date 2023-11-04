using PM.Application.Common.Models.Employee;
using PM.Domain.Common.Enums;
using Status = PM.Domain.Common.Enums.Status;

namespace PM.Application.Common.Models.Task;

/// <summary>
/// Represents a result object for a task.
/// </summary>
public class TaskResult
{
    /// <summary>
    /// Gets or sets the ID of the task.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the task.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the author of the task.
    /// </summary>
    public UserResult Author { get; set; } = null!;

    /// <summary>
    /// Gets or sets the executor of the task.
    /// </summary>
    public UserResult Executor { get; set; } = null!;

    /// <summary>
    /// Gets or sets the status of the task.
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// Gets or sets the priority of the task.
    /// </summary>
    public Priority Priority { get; set; }
}

