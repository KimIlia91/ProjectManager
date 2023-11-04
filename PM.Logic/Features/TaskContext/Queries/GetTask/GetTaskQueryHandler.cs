using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Resources;
using PM.Application.Features.TaskContext.Dtos;

namespace PM.Application.Features.TaskContext.Queries.GetTask;

internal sealed class GetTaskQueryHandler 
    : IRequestHandler<GetTaskQuery, ErrorOr<GetTaskResult>>
{
    private readonly ITaskRepository _taskRepository;

    public GetTaskQueryHandler(
        ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

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
