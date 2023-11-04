using PM.Application.Common.Enums;
using PM.Application.Common.Models.Task;
using PM.Application.Common.Specifications.TaskSpecifications;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Common.Extensions;

/// <summary>
/// Extensions for filtering and sorting task queries.
/// </summary>
internal static class TaskQueryExtensions
{
    /// <summary>
    /// Filters a queryable of tasks based on the provided <paramref name="filter"/>.
    /// </summary>
    /// <param name="tasks">The source queryable of tasks.</param>
    /// <param name="filter">The task filter to apply.</param>
    /// <returns>A filtered queryable of tasks.</returns>
    public static IQueryable<Task> Filter(
      this IQueryable<Task> tasks,
      TaskFilter filter)
    {
        return tasks
            .Where(new TaskPrioritySpecification(filter.Priority).ToExpression())
            .Where(new TaskExecutorSpecification(filter.ExecutorId).ToExpression())
            .Where(new TaskAuthorSpecification(filter.AuthorId).ToExpression())
            .Where(new TaskStatusSpecification(filter.Status).ToExpression());
    }

    /// <summary>
    /// Sorts a queryable of tasks based on the provided <paramref name="sortBy"/> string.
    /// </summary>
    /// <param name="taskQuery">The source queryable of tasks to be sorted.</param>
    /// <param name="sortBy">A comma-separated string specifying sorting properties and directions.</param>
    /// <returns>A sorted queryable of tasks.</returns>
    public static IQueryable<Task> Sort(
        this IQueryable<Task> taskQuery,
        string? sortBy)
    {
        if (string.IsNullOrEmpty(sortBy)) return taskQuery;

        var sortPairs = sortBy.Split(',', StringSplitOptions.RemoveEmptyEntries);

        var sortedTaskQuery = taskQuery.OrderBy(p => 0);

        foreach (var sortProperty in sortPairs)
        {
            var property = sortProperty;
            var sortOrder = SortStates.Ascending;

            if (sortProperty.EndsWith(".Desc"))
            {
                property = sortProperty[..^5];
                sortOrder = SortStates.Descending;
            }

            switch (property)
            {
                case "priority":
                    sortedTaskQuery = sortOrder == SortStates.Descending
                        ? sortedTaskQuery.ThenByDescending(p => p.Priority)
                        : sortedTaskQuery.ThenBy(p => p.Priority);
                    break;

                case "name":
                    sortedTaskQuery = sortOrder == SortStates.Descending
                        ? sortedTaskQuery.ThenByDescending(p => p.Name)
                        : sortedTaskQuery.ThenBy(p => p.Name);
                    break;

                case "status":
                    sortedTaskQuery = sortOrder == SortStates.Descending
                       ? sortedTaskQuery.ThenByDescending(p => p.Status)
                       : sortedTaskQuery.ThenBy(p => p.Status);
                    break;

                case "author.LastName":
                    sortedTaskQuery = sortOrder == SortStates.Descending
                       ? sortedTaskQuery.ThenByDescending(p => p.Author.LastName)
                       : sortedTaskQuery.ThenBy(p => p.Author.LastName);
                    break;

                case "author.FirstName":
                    sortedTaskQuery = sortOrder == SortStates.Descending
                       ? sortedTaskQuery.ThenByDescending(p => p.Author.FirstName)
                       : sortedTaskQuery.ThenBy(p => p.Author.FirstName);
                    break;

                case "executor.FirstName":
                    sortedTaskQuery = sortOrder == SortStates.Descending
                       ? sortedTaskQuery.ThenByDescending(p => p.Executor.FirstName)
                       : sortedTaskQuery.ThenBy(p => p.Executor.FirstName);
                    break;

                case "executor.LastName":
                    sortedTaskQuery = sortOrder == SortStates.Descending
                       ? sortedTaskQuery.ThenByDescending(p => p.Executor.LastName)
                       : sortedTaskQuery.ThenBy(p => p.Executor.LastName);
                    break;

                default:
                    break;
            }
        }

        return sortedTaskQuery;
    }
}
