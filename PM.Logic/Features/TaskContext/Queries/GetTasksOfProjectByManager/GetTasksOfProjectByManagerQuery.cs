using ErrorOr;
using MediatR;
using PM.Application.Common.Models.Task;

namespace PM.Application.Features.TaskContext.Queries.GetTasksOfProjectByManager;

public sealed record GetTasksOfProjectByManagerQuery(
    int ProjectId,
    TaskFilter Filter,
    string? SortBy) : IRequest<ErrorOr<List<TaskResult>>>;
