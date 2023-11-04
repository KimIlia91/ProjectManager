using ErrorOr;
using MediatR;
using PM.Application.Common.Models.Task;

namespace PM.Application.Features.TaskContext.Queries.GetProjectTasks;

/// <summary>
/// Represents a query to retrieve tasks associated with a project.
/// </summary>
/// <param name="ProjectId">The unique identifier of the project for which tasks are requested.</param>
/// <param name="Filter">The filter criteria to apply when retrieving tasks.</param>
/// <param name="SortBy">The sorting criteria for the retrieved tasks (optional).</param>
public sealed record GetProjectTasksQuery(
    int ProjectId,
    TaskFilter Filter,
    string? SortBy) : IRequest<ErrorOr<List<TaskResult>>>;
