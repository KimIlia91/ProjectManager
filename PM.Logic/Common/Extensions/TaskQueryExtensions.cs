using PM.Application.Common.Enums;
using PM.Application.Common.Models.Task;
using PM.Application.Common.Specifications.TaskSpecifications;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Common.Extensions;

internal static class TaskQueryExtensions
{
    public static IQueryable<Task> Where(
      this IQueryable<Task> tasks,
      TaskFilter filter)
    {
        return tasks
            .Where(new TaskPrioritySpecification(filter.Priority).ToExpression())
            .Where(new TaskExecutorSpecification(filter.ExecutorId).ToExpression())
            .Where(new TaskAuthorSpecification(filter.AuthorId).ToExpression())
            .Where(new TaskStatusSpecification(filter.Status).ToExpression());
    }

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
