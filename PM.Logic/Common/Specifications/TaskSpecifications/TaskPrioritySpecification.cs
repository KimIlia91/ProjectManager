using PM.Application.Common.Specifications.ISpecifications;
using PM.Domain.Common.Enums;
using System.Linq.Expressions;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Common.Specifications.TaskSpecifications;

internal sealed class TaskPrioritySpecification : ISpecification<Task>
{
    private readonly Priority? _priority;

    public TaskPrioritySpecification(
        Priority? priority)
    {
        _priority = priority;
    }

    public Expression<Func<Task, bool>> ToExpression()
    {
        return p => (!_priority.HasValue || p.Priority == _priority);
    }
}
