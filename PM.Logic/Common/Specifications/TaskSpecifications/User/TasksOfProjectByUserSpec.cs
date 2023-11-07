using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Specifications.ISpecifications;
using System.Linq.Expressions;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Common.Specifications.TaskSpecifications.User;

internal class TasksOfProjectByUserSpec : ISpecification<Task>
{
    private readonly int _projectId;
    private readonly int _userId;
    private readonly ICurrentUserService _currentUserService;

    public TasksOfProjectByUserSpec(
        int projectId,
        ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
        _userId = _currentUserService.UserId;
        _projectId = projectId;
    }

    public Expression<Func<Task, bool>> ToExpression()
    {
        if (_currentUserService.IsSupervisor)
            return t => t.Id == _projectId;

        return t => t.ProjectId == _projectId &&
                  (t.Executor != null && t.Executor.Id == _userId ||
                   t.Author != null && t.Author.Id == _userId);
    }
}
