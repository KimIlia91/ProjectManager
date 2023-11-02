using ErrorOr;
using MediatR;
using PM.Application.Common.Extensions;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.TaskContext.Dtos;

namespace PM.Application.Features.TaskContext.Queries.GetTaskList;

internal sealed class GetTaskListQueryHandler
    : IRequestHandler<GetTaskListQuery, ErrorOr<List<GetTaskResult>>>
{
    private readonly ITaskRepository _taskRepository;

    public GetTaskListQueryHandler(
        ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<ErrorOr<List<GetTaskResult>>> Handle(
        GetTaskListQuery query, 
        CancellationToken cancellationToken)
    {
        var taskQuery = _taskRepository
            .GetQuiery()
            .Where(query.Filter)
            .Sort(query.SortBy);

        return await _taskRepository
            .ToListResultAsync<GetTaskResult>(taskQuery, cancellationToken);
    }
}
