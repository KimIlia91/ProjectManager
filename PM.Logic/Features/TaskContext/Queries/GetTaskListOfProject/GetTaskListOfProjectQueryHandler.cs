using ErrorOr;
using MediatR;
using PM.Application.Common.Extensions;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Models.Task;

namespace PM.Application.Features.TaskContext.Queries.GetTaskListOfProject;

internal sealed class GetTaskListOfProjectQueryHandler
    : IRequestHandler<GetTaskListOfProjectQuery, ErrorOr<List<TaskResult>>>
{
    private readonly ITaskRepository _taskRepository;

    public GetTaskListOfProjectQueryHandler(
        ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<ErrorOr<List<TaskResult>>> Handle(
        GetTaskListOfProjectQuery query,
        CancellationToken cancellationToken)
    {
        var taskQuery = _taskRepository
          .GetQuery(asNoTracking: true)
          .Where(t => t.ProjectId == query.ProjectId)
          .Filter(query.Filter)
          .Sort(query.SortBy);

        return await _taskRepository
            .ToListResultAsync<TaskResult>(taskQuery, cancellationToken);
    }
}
