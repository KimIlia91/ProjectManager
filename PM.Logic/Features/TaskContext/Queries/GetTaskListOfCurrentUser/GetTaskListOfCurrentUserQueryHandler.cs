using ErrorOr;
using MediatR;
using PM.Application.Common.Extensions;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Models.Task;

namespace PM.Application.Features.TaskContext.Queries.GetUserTaskList;

internal sealed class GetTaskListOfCurrentUserQueryHandler
    : IRequestHandler<GetTaskListOfCurrentUserQuery, ErrorOr<List<TaskResult>>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetTaskListOfCurrentUserQueryHandler(
        ITaskRepository taskRepository,
        ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
        _taskRepository = taskRepository;
    }

    public async Task<ErrorOr<List<TaskResult>>> Handle(
        GetTaskListOfCurrentUserQuery query, 
        CancellationToken cancellationToken)
    {
        var taskQuery = _taskRepository
            .GetQuery()
            .Where(t => t.Author.Id == _currentUserService.UserId ||
                        t.Executor.Id == _currentUserService.UserId)
            .Filter(query.Filter)
            .Sort(query.Sort);

        return await _taskRepository
            .ToListResultAsync<TaskResult>(taskQuery, cancellationToken);
    }
}
