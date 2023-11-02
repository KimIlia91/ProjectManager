using PM.Application.Common.Specifications.ISpecifications;
using PM.Domain.Common.Enums;
using System.Linq.Expressions;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Common.Specifications.TaskSpecifications;

internal sealed class TaskStatusSpecification : ISpecification<Task>
{
    private readonly Status? _status;

    public TaskStatusSpecification(
        Status? status)
    {
        _status = status;
    }

    public Expression<Func<Task, bool>> ToExpression()
    {
        return p => (!_status.HasValue || p.Status == _status);
    }
}
