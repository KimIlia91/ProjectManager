using ErrorOr;
using MediatR;
using PM.Application.Common.Extensions;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.TaskContext.Dtos;

namespace PM.Application.Features.TaskContext.Queries.GetTaskList;

/// <summary>
/// Handles the request to retrieve a list of tasks with optional filtering and sorting.
/// </summary>
internal sealed class GetTaskListQueryHandler
    : IRequestHandler<GetTaskListQuery, ErrorOr<List<GetTaskListResult>>>
{
    private readonly ITaskRepository _taskRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTaskListQueryHandler"/> class.
    /// </summary>
    /// <param name="taskRepository">The repository for tasks.</param>
    public GetTaskListQueryHandler(
        ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    /// <summary>
    /// Handles the request to retrieve a list of tasks based on the provided query parameters.
    /// </summary>
    /// <param name="query">The query with filtering and sorting parameters.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of tasks based on the query parameters.</returns>
    public async Task<ErrorOr<List<GetTaskListResult>>> Handle(
        GetTaskListQuery query,
        CancellationToken cancellationToken)
    {
        var taskQuery = _taskRepository
            .GetQuery()
            .Filter(query.Filter)
            .Sort(query.SortBy);

        return await _taskRepository
            .ToListResultAsync<GetTaskListResult>(taskQuery, cancellationToken);
    }
}
