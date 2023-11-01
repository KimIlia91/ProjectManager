using ErrorOr;
using PM.Domain.Common.Enums;
using TaskStatus = PM.Domain.Common.Enums.TaskStatus;

namespace PM.Domain.Entities;

public sealed class Task : BaseEntity
{
    public string Name { get; private set; } = null!;

    public Employee? Author { get; private set; }

    public Employee? Executor { get; private set; }

    public Project Project { get; private set; }

    public string Comment { get; private set; } = null!;

    public TaskStatus Status { get; private set; }

    public ProjectPriority Priority { get; private set; }

    private Task() { }

    internal Task(
        string name,
        Employee? author,
        Employee? executor,
        Project project,
        string comment,
        TaskStatus taskStatus,
        ProjectPriority priority)
    {
        Name = name;
        Author = author;
        Executor = executor;
        Project = project;
        Comment = comment;
        Status = taskStatus;
        Priority = priority;
    }

    public static ErrorOr<Task> Create(
        string name,
        Employee? author,
        Employee? executor,
        Project project,
        string comment,
        TaskStatus taskStatus,
        ProjectPriority priority)
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

    public ErrorOr<Task> Update(
        string name,
        Employee author,
        Employee executor,
        string comment,
        TaskStatus taskStatus,
        ProjectPriority priority)
    {
        Name = name;
        Author = author;
        Executor = executor;
        Comment = comment;
        Status = taskStatus;
        Priority = priority;

        return this;
    }


    public void RemoveAuthor()
    {
        Author = null;
    }
}