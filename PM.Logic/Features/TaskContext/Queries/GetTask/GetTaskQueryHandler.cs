using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.TaskContext.Dtos;

namespace PM.Application.Features.TaskContext.Queries.GetTask;

internal sealed class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, ErrorOr<GetTaskResult>>
{
    private readonly ITaskRepository _taskRepository;

    public GetTaskQueryHandler(
        ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public Task<ErrorOr<GetTaskResult>> Handle(
        GetTaskQuery query, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
