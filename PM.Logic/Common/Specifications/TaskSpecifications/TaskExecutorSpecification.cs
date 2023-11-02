using PM.Application.Common.Specifications.ISpecifications;
using System.Linq.Expressions;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Common.Specifications.TaskSpecifications;

internal class TaskExecutorSpecification : ISpecification<Task>
{
    private readonly int? _executorId;

    public TaskExecutorSpecification(
        int? executorId)
    {
        _executorId = executorId;
    }

    public Expression<Func<Task, bool>> ToExpression()
    {
        return p => (!_executorId.HasValue
            || (_executorId.HasValue && _executorId == 0 && p.Executor == null)
            || p.Executor.Id == _executorId);
    }
}
