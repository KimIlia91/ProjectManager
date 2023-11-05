using ErrorOr;
using MediatR;
using PM.Application.Common.Extensions;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Models.Task;

namespace PM.Application.Features.TaskContext.Queries.GetProjectTasks;

/// <summary>
/// Handles the retrieval of tasks associated with a project based on the specified query.
/// </summary>
internal sealed class GetProjectTasksQueryHandler
    : IRequestHandler<GetProjectTasksQuery, ErrorOr<List<TaskResult>>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ICurrentUserService _currentUserService;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProjectTasksQueryHandler"/> class.
    /// </summary>
    /// <param name="taskRepository">The repository for tasks.</param>
    public GetProjectTasksQueryHandler(
        ITaskRepository taskRepository,
        ICurrentUserService currentUserService)
    {
        _taskRepository = taskRepository;
        _currentUserService = currentUserService;
    }

    /// <summary>
    /// Handles the processing of the query to retrieve tasks associated with a project.
    /// </summary>
    /// <param name="query">The query specifying the project and filtering criteria.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of task results or an error result.</returns>
    public async Task<ErrorOr<List<TaskResult>>> Handle(
        GetProjectTasksQuery query,
        CancellationToken cancellationToken)
    {
        var taskQuery = _taskRepository
            .GetQuery()
            .Where(t => t.Project.Id == query.ProjectId)
            .Filter(query.Filter)
            .Sort(query.SortBy);

        return await _taskRepository
            .ToListResultAsync<TaskResult>(taskQuery, cancellationToken);
    }
}

