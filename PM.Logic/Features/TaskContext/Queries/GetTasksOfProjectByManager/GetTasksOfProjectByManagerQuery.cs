using ErrorOr;
using MediatR;
using PM.Application.Common.Models.Task;

namespace PM.Application.Features.TaskContext.Queries.GetTasksOfProjectByManager;

public sealed class GetTasksOfProjectByManagerQuery : IRequest<ErrorOr<List<TaskResult>>>
{
    public int ProjectId { get; set; }

    public TaskFilter Filter { get; set; } = new();

    public string? SortBy { get; set; }
}
