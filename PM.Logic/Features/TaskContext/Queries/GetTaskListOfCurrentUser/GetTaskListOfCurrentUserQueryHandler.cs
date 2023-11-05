using ErrorOr;
using MediatR;
using PM.Application.Common.Extensions;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Models.Task;
using PM.Application.Common.Specifications.TaskSpecifications;

namespace PM.Application.Features.TaskContext.Queries.GetTaskListOfCurrentUser;

internal sealed class GetTaskListOfCurrentUserQueryHandler
    : IRequestHandler<GetTaskListOfCurrentUserQuery, ErrorOr<List<TaskResult>>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ICurrentUserService _currentUser;

    public GetTaskListOfCurrentUserQueryHandler(
        ITaskRepository taskRepository,
        ICurrentUserService currentUser)
    {
        _currentUser = currentUser;
        _taskRepository = taskRepository;
    }

    public async Task<ErrorOr<List<TaskResult>>> Handle(
        GetTaskListOfCurrentUserQuery query,
        CancellationToken cancellationToken)
    {
        var getCurrentUserTasks = new GetTasksOfUserSpec(_currentUser);

        var taskQuery = _taskRepository
            .GetQuery()
            .Where(getCurrentUserTasks.ToExpression())
            .Filter(query.Filter)
            .Sort(query.Sort);

        return await _taskRepository
            .ToListResultAsync<TaskResult>(taskQuery, cancellationToken);
    }
}
