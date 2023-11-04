using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Resources;
using PM.Application.Features.TaskContext.Dtos;

namespace PM.Application.Features.TaskContext.Queries.GetTask;

/// <summary>
/// Handles the retrieval of a task based on a query by its unique identifier.
/// </summary>
internal sealed class GetTaskQueryHandler
    : IRequestHandler<GetTaskQuery, ErrorOr<GetTaskResult>>
{
    private readonly ITaskRepository _taskRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTaskQueryHandler"/> class.
    /// </summary>
    /// <param name="taskRepository">The repository for tasks.</param>
    public GetTaskQueryHandler(
        ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    /// <summary>
    /// Handles the execution of the <see cref="GetTaskQuery"/> to retrieve a task.
    /// </summary>
    /// <param name="query">The query specifying the task identifier.</param>
    /// <param name="cancellationToken">The token for cancelling the operation.</param>
    /// <returns>An <see cref="ErrorOr{T}"/> containing the result of the query.</returns>
    public async Task<ErrorOr<GetTaskResult>> Handle(
        GetTaskQuery query,
        CancellationToken cancellationToken)
    {
        var task = await _taskRepository
            .GetTaskResultByIdAsync(query.Id, cancellationToken);

        if (task is null)
            return Error.NotFound(ErrorsResource.NotFound, nameof(query.Id));

        return task;
    }
}