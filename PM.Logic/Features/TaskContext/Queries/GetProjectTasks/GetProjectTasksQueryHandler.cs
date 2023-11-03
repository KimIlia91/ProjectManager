using ErrorOr;
using MediatR;
using PM.Application.Common.Extensions;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Models.Task;

namespace PM.Application.Features.TaskContext.Queries.GetProjectTasks;

internal sealed class GetProjectTasksQueryHandler
    : IRequestHandler<GetProjectTasksQuery, ErrorOr<List<TaskResult>>>
{
    private readonly ITaskRepository _taskRepository;

    public GetProjectTasksQueryHandler(
        ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<ErrorOr<List<TaskResult>>> Handle(
        GetProjectTasksQuery query,
        CancellationToken cancellationToken)
    {
        var taskQuery = _taskRepository
            .GetQuiery()
            .Where(t => t.Project.Id == query.ProjectId)
            .Filter(query.Filter)
            .Sort(query.SortBy);

        return await _taskRepository
            .ToListResultAsync<TaskResult>(taskQuery, cancellationToken);
    }
}
