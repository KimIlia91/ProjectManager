using PM.Application.Common.Specifications.ISpecifications;
using PM.Domain.Common.Enums;
using System.Linq.Expressions;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Common.Specifications.TaskSpecifications.Filter;

/// <summary>
/// Represents a specification for filtering tasks by priority.
/// </summary>
internal sealed class TaskPrioritySpec : ISpecification<Task>
{
    private readonly Priority? _priority;

    /// <summary>
    /// Initializes a new instance of the <see cref="TaskPrioritySpec"/> class.
    /// </summary>
    /// <param name="priority">The priority to filter by.</param>
    public TaskPrioritySpec(Priority? priority)
    {
        _priority = priority;
    }

    /// <summary>
    /// Converts the specification to an expression.
    /// </summary>
    /// <returns>An expression representing the filtering condition for tasks based on the priority.</returns>
    public Expression<Func<Task, bool>> ToExpression()
    {
        return p => !_priority.HasValue || p.Priority == _priority;
    }
}
