using PM.Application.Common.Models.Employee;
using PM.Domain.Common.Enums;

namespace PM.Application.Features.TaskContext.Dtos;

/// <summary>
/// Represents a task item in a task list.
/// </summary>
public class GetTaskListResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the task.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the task.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the author of the task.
    /// </summary>
    public UserResult Author { get; set; } = new UserResult();

    /// <summary>
    /// Gets or sets the executor of the task.
    /// </summary>
    public UserResult Executor { get; set; } = new UserResult();

    /// <summary>
    /// Gets or sets the comment associated with the task.
    /// </summary>
    public string Comment { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the status of the task.
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the priority of the task.
    /// </summary>
    public Priority Priority { get; set; }
}
