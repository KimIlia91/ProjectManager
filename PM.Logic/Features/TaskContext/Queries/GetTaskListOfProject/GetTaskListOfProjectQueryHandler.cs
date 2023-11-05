using ErrorOr;
using MediatR;
using PM.Application.Common.Extensions;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Models.Task;
using PM.Application.Common.Specifications.TaskSpecifications;
using System.Linq;

namespace PM.Application.Features.TaskContext.Queries.GetTaskListOfProject;

internal sealed class GetTaskListOfProjectQueryHandler
    : IRequestHandler<GetTaskListOfProjectQuery, ErrorOr<List<TaskResult>>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ICurrentUserService _currentUser;

    public GetTaskListOfProjectQueryHandler(
        ITaskRepository taskRepository,
        ICurrentUserService currentUser)
    {
        _currentUser = currentUser;
        _taskRepository = taskRepository;
    }

    public async Task<ErrorOr<List<TaskResult>>> Handle(
        GetTaskListOfProjectQuery query,
        CancellationToken cancellationToken)
    {
        var taskListOfProjectByManager = new GetTaskListOfProjectByManagerSpec(
            query.ProjectId,
            _currentUser);

        var taskQuery = _taskRepository
          .GetQuery(asNoTracking: true)
          .Where(taskListOfProjectByManager.ToExpression())
          .Filter(query.Filter)
          .Sort(query.SortBy);

        return await _taskRepository
            .ToListResultAsync<TaskResult>(taskQuery, cancellationToken);
    }
}
