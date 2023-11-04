using ErrorOr;
using MediatR;
using PM.Application.Common.Models.Task;
using PM.Application.Features.TaskContext.Dtos;

namespace PM.Application.Features.TaskContext.Queries.GetTaskList;

/// <summary>
/// Represents a query to retrieve a list of tasks with optional filtering and sorting.
/// </summary>
public sealed class GetTaskListQuery : IRequest<ErrorOr<List<GetTaskListResult>>>
{
    /// <summary>
    /// Gets or sets the filtering options for the query. Default is an empty filter.
    /// </summary>
    public TaskFilter Filter { get; set; } = new();

    /// <summary>
    /// Gets or sets the field by which to sort the results. Leave as null for no sorting.
    /// </summary>
    public string? SortBy { get; set; }
}
