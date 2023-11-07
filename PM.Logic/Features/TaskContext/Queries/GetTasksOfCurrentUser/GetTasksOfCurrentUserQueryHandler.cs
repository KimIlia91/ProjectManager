using ErrorOr;
using MediatR;
using PM.Application.Common.Extensions;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Models.Task;
using PM.Application.Common.Specifications.TaskSpecifications;
using PM.Application.Common.Specifications.TaskSpecifications.User;

namespace PM.Application.Features.TaskContext.Queries.GetTasksOfCurrentUser;

internal sealed class GetTasksOfCurrentUserQueryHandler
    : IRequestHandler<GetTasksOfCurrentUserQuery, ErrorOr<List<TaskResult>>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ICurrentUserService _currentUser;

    public GetTasksOfCurrentUserQueryHandler(
        ITaskRepository taskRepository,
        ICurrentUserService currentUser)
    {
        _currentUser = currentUser;
        _taskRepository = taskRepository;
    }

    public async Task<ErrorOr<List<TaskResult>>> Handle(
        GetTasksOfCurrentUserQuery query,
        CancellationToken cancellationToken)
    {
        var getCurrentUserTasks = new TasksOfUserSpec(_currentUser);

        var taskQuery = _taskRepository
            .GetQuery()
            .Where(getCurrentUserTasks.ToExpression())
            .Filter(query.Filter)
            .Sort(query.Sort);

        return await _taskRepository
            .ToListResultAsync<TaskResult>(taskQuery, cancellationToken);
    }
}
