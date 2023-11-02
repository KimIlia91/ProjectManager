using ErrorOr;
using MediatR;
using PM.Application.Common.Models.Task;
using PM.Application.Features.TaskContext.Dtos;

namespace PM.Application.Features.TaskContext.Queries.GetTaskList;

public sealed class GetTaskListQuery
    : IRequest<ErrorOr<List<GetTaskResult>>>
{
    public TaskFilter Filter { get; set; } = new();

    public string? SortBy { get; set; }
}
