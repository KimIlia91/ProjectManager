using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Specifications.ISpecifications;
using System.Linq.Expressions;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Common.Specifications.TaskSpecifications;

internal class GetTaskByManagerSpec : ISpecification<Task>
{
    private readonly int _taskId;
    private readonly int _managerId;

    public GetTaskByManagerSpec(
        int taskId,
        int managerId)
    {
        _managerId = managerId;
        _taskId = taskId;
    }

    public Expression<Func<Task, bool>> ToExpression()
    {
        return t => t.Id == _taskId &&
                    t.Project.Manager != null &&
                    t.Project.Manager.Id == _managerId;
    }
}
