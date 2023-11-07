using PM.Application.Common.Specifications.ISpecifications;
using System.Linq.Expressions;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Common.Specifications.TaskSpecifications.Filter;
/// <summary>
/// Represents a specification for filtering tasks by executor.
/// </summary>
internal class TaskExecutorSpec : ISpecification<Task>
{
    private readonly int? _executorId;

    /// <summary>
    /// Initializes a new instance of the <see cref="TaskExecutorSpec"/> class.
    /// </summary>
    /// <param name="executorId">The ID of the executor to filter by.</param>
    public TaskExecutorSpec(int? executorId)
    {
        _executorId = executorId;
    }

    /// <summary>
    /// Converts the specification to an expression.
    /// </summary>
    /// <returns>An expression representing the filtering condition for tasks based on the executor's ID.</returns>
    public Expression<Func<Task, bool>> ToExpression()
    {
        return p => !_executorId.HasValue
            || _executorId.HasValue && _executorId == 0 && p.Executor == null
            || p.Executor.Id == _executorId;
    }
}
