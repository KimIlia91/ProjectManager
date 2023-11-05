using ErrorOr;
using MediatR;
using PM.Application.Common.Models.Task;

namespace PM.Application.Features.TaskContext.Queries.GetTaskListOfCurrentUser;

public sealed class GetTaskListOfCurrentUserQuery : IRequest<ErrorOr<List<TaskResult>>>
{
    public TaskFilter Filter { get; set; } = new();

    public string? Sort { get; set; }
}