using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Specifications.ISpecifications;
using System.Linq.Expressions;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Common.Specifications.TaskSpecifications.User;

internal class TasksOfUserSpec : ISpecification<Task>
{
    private readonly int _userId;
    private readonly ICurrentUserService _currentUserService;

    public TasksOfUserSpec(
        ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
        _userId = _currentUserService.UserId;
    }

    public Expression<Func<Task, bool>> ToExpression()
    {
        return t => t.Author != null && t.Author.Id == _userId ||
                    t.Executor != null && t.Executor.Id == _userId ||
                    t.Project.ManagerId == _userId;
    }
}
