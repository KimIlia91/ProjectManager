using PM.Application.Common.Specifications.ISpecifications;
using PM.Domain.Common.Enums;
using System.Linq.Expressions;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Common.Specifications.TaskSpecifications.Filter;

/// <summary>
/// Represents a specification for filtering tasks by status.
/// </summary>
internal sealed class TaskStatusSpec : ISpecification<Task>
{
    private readonly Status? _status;

    /// <summary>
    /// Initializes a new instance of the <see cref="TaskStatusSpec"/> class.
    /// </summary>
    /// <param name="status">The status to filter by.</param>
    public TaskStatusSpec(Status? status)
    {
        _status = status;
    }

    /// <summary>
    /// Converts the specification to an expression.
    /// </summary>
    /// <returns>An expression representing the filtering condition for tasks based on the status.</returns>
    public Expression<Func<Task, bool>> ToExpression()
    {
        return p => !_status.HasValue || p.Status == _status;
    }
}
