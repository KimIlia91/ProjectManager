using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Models.Task;
using PM.Application.Common.Resources;

namespace PM.Application.Features.TaskContext.Queries.GetTaskOfCurrentUser;

internal sealed class GetTaskOfUserCurrentQueryHandler
    : IRequestHandler<GetTaskOfCurrentUserQuery, ErrorOr<TaskResult>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ICurrentUserService _currentUser;

    public GetTaskOfUserCurrentQueryHandler(
        ITaskRepository taskRepository,
        ICurrentUserService currentUser)
    {
        _currentUser = currentUser;
        _taskRepository = taskRepository;
    }

    public async Task<ErrorOr<TaskResult>> Handle(
        GetTaskOfCurrentUserQuery query, 
        CancellationToken cancellationToken)
    {
        var task = await _taskRepository
            .GetTaskOfUserByIdAsync(query.TaskId, _currentUser.UserId, cancellationToken);

        if (task is null)
            return Error.NotFound(ErrorsResource.NotFound, nameof(query.TaskId));

        return task;
    }
}
