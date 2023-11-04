using PM.Application.Common.Models.Employee;
using PM.Application.Common.Models.Project;
using PM.Domain.Common.Enums;
using Status = PM.Domain.Common.Enums.Status;

namespace PM.Application.Features.TaskContext.Dtos;

/// <summary>
/// Represents a detailed task result.
/// </summary>
public class GetTaskResult
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
    /// Gets or sets the project to which the task is associated.
    /// </summary>
    public ProjectResult Project { get; set; } = new ProjectResult();

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

