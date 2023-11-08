using ErrorOr;
using PM.Domain.Common.Enums;

namespace PM.Domain.Entities;

/// <summary>
/// Represents a task.
/// </summary>
public sealed class Task : BaseEntity
{
    /// <summary>
    /// Gets or private sets the name of the task.
    /// </summary>
    public string Name { get; private set; } = null!;

    /// <summary>
    /// Gets or private sets the author id of the task.
    /// </summary>
    public int? AuthorId { get; private set; }

    /// <summary>
    /// Gets or private sets the author of the task.
    /// </summary>
    public User? Author { get; private set; }

    /// <summary>
    /// Gets or private sets the executor ID of the task.
    /// </summary>
    public int? ExecutorId { get; private set; }

    /// <summary>
    /// Gets or private sets the executor of the task.
    /// </summary>
    public User? Executor { get; private set; }

    /// <summary>
    /// Gets or private sets the projectID of the task.
    /// </summary>
    public int ProjectId { get; private set; }

    /// <summary>
    /// Gets or private sets the project to which the task belongs.
    /// </summary>
    public Project Project { get; private set; }

    /// <summary>
    /// Gets or private sets additional comments related to the task.
    /// </summary>
    public string? Comment { get; private set; }

    /// <summary>
    /// Gets or private sets the status of the task.
    /// </summary>
    public Status Status { get; private set; }

    /// <summary>
    /// Gets or private sets the priority of the task.
    /// </summary>
    public Priority Priority { get; private set; }

    private Task() { }

    /// <summary>
    /// Initializes a new task.
    /// </summary>
    /// <param name="name">The name of the task.</param>
    /// <param name="author">The author of the task.</param>
    /// <param name="executor">The executor of the task.</param>
    /// <param name="project">The project to which the task belongs.</param>
    /// <param name="comment">Additional comments related to the task.</param>
    /// <param name="taskStatus">The status of the task.</param>
    /// <param name="priority">The priority of the task.</param>
    internal Task(
        string name,
        User? author,
        User? executor,
        Project project,
        string? comment,
        Status taskStatus,
        Priority priority)
    {
        Name = name;
        Author = author;
        Executor = executor;
        Project = project;
        Comment = comment;
        Status = taskStatus;
        Priority = priority;
    }

    /// <summary>
    /// Creates a new task.
    /// </summary>
    /// <param name="name">The name of the task.</param>
    /// <param name="author">The author of the task.</param>
    /// <param name="executor">The executor of the task.</param>
    /// <param name="project">The project to which the task belongs.</param>
    /// <param name="comment">Additional comments related to the task.</param>
    /// <param name="taskStatus">The status of the task.</param>
    /// <param name="priority">The priority of the task.</param>
    /// <returns>The newly created task.</returns>
    public static ErrorOr<Task> Create(
        string name,
        User? author,
        User? executor,
        Project project,
        string? comment,
        Status taskStatus,
        Priority priority)
    {
        return new Task(
            name,
            author,
            executor,
            project,
            comment,
            taskStatus,
            priority);
    }

    /// <summary>
    /// Updates the task with new details.
    /// </summary>
    /// <param name="name">The new name of the task.</param>
    /// <param name="author">The new author of the task.</param>
    /// <param name="executor">The new executor of the task.</param>
    /// <param name="comment">The new comment related to the task.</param>
    /// <param name="taskStatus">The new status of the task.</param>
    /// <param name="priority">The new priority of the task.</param>
    /// <returns>The updated task.</returns>
    public ErrorOr<Task> Update(
        string name,
        User? author,
        User? executor,
        string? comment,
        Status taskStatus,
        Priority priority)
    {
        Name = name;
        Author = author;
        Executor = executor;
        Comment = comment;
        Status = taskStatus;
        Priority = priority;

        return this;
    }

    /// <summary>
    /// Removes the author from the task.
    /// </summary>
    public void RemoveAuthor()
    {
        Author = null;
    }

    /// <summary>
    /// Change the task status.
    /// </summary>
    public void ChangeStatus(Status status)
    {
        Status = status;
    }
}