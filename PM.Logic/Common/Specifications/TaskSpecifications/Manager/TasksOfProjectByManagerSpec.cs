using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Specifications.ISpecifications;
using System.Linq.Expressions;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Common.Specifications.TaskSpecifications.Manager;

internal class TasksOfProjectByManagerSpec : ISpecification<Task>
{
    private readonly int _projectId;
    private readonly int _managerId;
    private readonly ICurrentUserService _currentUserService;

    public TasksOfProjectByManagerSpec(
        int projectId,
        ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
        _managerId = _currentUserService.UserId;
        _projectId = projectId;
    }

    public Expression<Func<Task, bool>> ToExpression()
    {
        if (_currentUserService.IsSupervisor)
            return t => t.ProjectId == _projectId;

        return t => t.ProjectId == _projectId &&
                    t.Project.Manager != null &&
                    t.Project.Manager.Id == _managerId;
    }
}
