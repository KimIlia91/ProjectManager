using PM.Domain.Common.Enums;

namespace PM.Application.Common.Models.Task;

public sealed class TaskFilter
{
    public Status? Status { get; set; }

    public Priority? Priority { get; set; }

    public int? AuthorId { get; set; }

    public int? ExecutorId { get; set; }
}