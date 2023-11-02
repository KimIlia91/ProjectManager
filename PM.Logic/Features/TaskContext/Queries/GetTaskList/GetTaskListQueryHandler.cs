using ErrorOr;
using MediatR;
using PM.Application.Common.Extensions;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Models.Project;
using PM.Application.Features.TaskContext.Dtos;

namespace PM.Application.Features.TaskContext.Queries.GetTaskList;

internal sealed class GetTaskListQueryHandler
    : IRequestHandler<GetTaskListQuery, ErrorOr<List<GetTaskListResult>>>
{
    private readonly ITaskRepository _taskRepository;

    public GetTaskListQueryHandler(
        ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<ErrorOr<List<GetTaskListResult>>> Handle(
        GetTaskListQuery query, 
        CancellationToken cancellationToken)
    {
        var taskQuery = _taskRepository
            .GetQuiery()
            .Where(query.Filter)
            .Sort(query.SortBy);

        return await _taskRepository
            .ToListResultAsync<GetTaskListResult>(taskQuery, cancellationToken);
    }
}
