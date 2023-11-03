using ErrorOr;
using PM.Domain.Common.Enums;

namespace PM.Domain.Entities;

public sealed class Task : BaseEntity
{
    public string Name { get; private set; } = null!;

    public User? Author { get; private set; }

    public User? Executor { get; private set; }

    public Project Project { get; private set; }

    public string? Comment { get; private set; }

    public Status Status { get; private set; }

    public Priority Priority { get; private set; }

    private Task() { }

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


    public void RemoveAuthor()
    {
        Author = null;
    }
}