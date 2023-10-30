using ErrorOr;
using PM.Domain.Common.Enums;

namespace PM.Domain.Entities;

public class Task : BaseEntity
{
    public string Name { get; private set; } = null!;

    public Employee Author { get; private set; }

    public Employee Executor { get; private set; }

    public int ProjectId { get; private set; }

    public string Comment { get; private set; } = null!;

    public Common.Enums.TaskStatus TaskStatus { get; private set; }

    public Priority Priority { get; private set; }

    private Task() { }

    internal Task(
        string name,
        Employee author,
        Employee executor,
        string comment,
        Common.Enums.TaskStatus taskStatus,
        Priority priority)
    {
        Name = name;
        Author = author;
        Executor = executor;
        Comment = comment;
        TaskStatus = taskStatus;
        Priority = priority;
    }

    public static ErrorOr<Task> Create(
        string name,
        Employee author,
        Employee executor,
        string comment,
        Common.Enums.TaskStatus taskStatus,
        Priority priority)
    {
        return new Task(
            name,
            author,
            executor,
            comment,
            taskStatus,
            priority);
    }

    public ErrorOr<Task> Update(
        string name,
        Employee author,
        Employee executor,
        string comment,
        Common.Enums.TaskStatus taskStatus,
        Priority priority)
    {
        Name = name;
        Author = author;
        Executor = executor;
        Comment = comment;
        TaskStatus = taskStatus;
        Priority = priority;

        return this;
    }
}