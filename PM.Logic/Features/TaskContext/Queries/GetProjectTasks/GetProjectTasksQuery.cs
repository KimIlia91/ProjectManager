using ErrorOr;
using MediatR;
using PM.Application.Common.Models.Task;

namespace PM.Application.Features.TaskContext.Queries.GetProjectTasks;

public sealed record GetProjectTasksQuery(
    int ProjectId,
    TaskFilter Filter,
    string? SortBy) : IRequest<ErrorOr<List<TaskResult>>>;