﻿using ErrorOr;
using MediatR;
using PM.Application.Common.Extensions;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Models.Task;
using PM.Application.Common.Specifications.TaskSpecifications;
using System.Linq;

namespace PM.Application.Features.TaskContext.Queries.GetTaskListOfProjectByUser;

/// <summary>
/// Handles the retrieval of tasks associated with a project based on the specified query.
/// </summary>
internal sealed class GetTaskListOfProjectByUserQueryHandler
    : IRequestHandler<GetTaskListOfProjectByUserQuery, ErrorOr<List<TaskResult>>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ICurrentUserService _currentUser;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTaskListOfProjectByUserQueryHandler"/> class.
    /// </summary>
    /// <param name="taskRepository">The repository for tasks.</param>
    public GetTaskListOfProjectByUserQueryHandler(
        ITaskRepository taskRepository,
        ICurrentUserService currentUserService)
    {
        _taskRepository = taskRepository;
        _currentUser = currentUserService;
    }

    /// <summary>
    /// Handles the processing of the query to retrieve tasks associated with a project.
    /// </summary>
    /// <param name="query">The query specifying the project and filtering criteria.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of task results or an error result.</returns>
    public async Task<ErrorOr<List<TaskResult>>> Handle(
        GetTaskListOfProjectByUserQuery query,
        CancellationToken cancellationToken)
    {
        var taskListOfProjectByUser = new GetTaskListOfProjectByUserSpec(
            query.ProjectId, 
            _currentUser);

        var taskQuery = _taskRepository
            .GetQuery(asNoTracking: true)
            .Where(taskListOfProjectByUser.ToExpression())
            .Filter(query.Filter)
            .Sort(query.SortBy);

        return await _taskRepository
            .ToListResultAsync<TaskResult>(taskQuery, cancellationToken);
    }
}

