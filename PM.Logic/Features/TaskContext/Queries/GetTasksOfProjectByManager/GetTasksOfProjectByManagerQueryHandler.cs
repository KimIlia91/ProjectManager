﻿using ErrorOr;
using MediatR;
using PM.Application.Common.Extensions;
using PM.Application.Common.Models.Task;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Specifications.TaskSpecifications.Manager;

namespace PM.Application.Features.TaskContext.Queries.GetTasksOfProjectByManager;

internal sealed class GetTasksOfProjectByManagerQueryHandler
    : IRequestHandler<GetTasksOfProjectByManagerQuery, ErrorOr<List<TaskResult>>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ICurrentUserService _currentUser;

    public GetTasksOfProjectByManagerQueryHandler(
        ITaskRepository taskRepository,
        ICurrentUserService currentUser)
    {
        _currentUser = currentUser;
        _taskRepository = taskRepository;
    }

    public async Task<ErrorOr<List<TaskResult>>> Handle(
        GetTasksOfProjectByManagerQuery query,
        CancellationToken cancellationToken)
    {
        var taskListOfProjectByManager = new TasksOfProjectByManagerSpec(
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
