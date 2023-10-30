using PM.Domain.Common.Enums;

namespace PM.Domain.Entities;

public sealed class Task : BaseEntity
{
    public string Name { get; private set; } = null!;

    public int AuthorId { get; private set; }

    public int ExecutorId { get; private set; }

    public string Comment { get; private set; } = null!;

    public TaskStatusEnum TaskStatus { get; private set; }

    public PriorityEnum Priority { get; private set; }

    internal Task(
        string name,
        int authorId,
        int executorId,
        string comment,
        TaskStatusEnum taskStatus,
        PriorityEnum priority)
    {
        Name = name;
        AuthorId = authorId;
        ExecutorId = executorId;
        Comment = comment;
        TaskStatus = taskStatus;
        Priority = priority;
    }

    public static Task Create(
        string name,
        int authorId,
        int executorId,
        string comment,
        TaskStatusEnum taskStatus,
        PriorityEnum priority)
    {
        return new Task(
            name,
            authorId,
            executorId,
            comment,
            taskStatus,
            priority);
    }
}
