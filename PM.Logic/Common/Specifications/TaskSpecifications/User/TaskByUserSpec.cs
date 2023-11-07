using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Specifications.ISpecifications;
using System.Linq.Expressions;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Common.Specifications.TaskSpecifications.User;

internal class TaskByUserSpec : ISpecification<Task>
{
    private readonly int _userId;
    private readonly int _taskId;
    private readonly ICurrentUserService _currentUserService;

    public TaskByUserSpec(
        int taskId,
        ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
        _userId = _currentUserService.UserId;
        _taskId = taskId;
    }

    public Expression<Func<Task, bool>> ToExpression()
    {
        return t => t.Id == _taskId &&
                    (t.Author != null && t.Author.Id == _userId ||
                    t.Executor != null && t.Executor.Id == _userId);
    }
}
