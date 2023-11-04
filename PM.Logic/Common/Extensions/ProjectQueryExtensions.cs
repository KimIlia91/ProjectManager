using PM.Application.Common.Enums;
using PM.Application.Common.Models.Project;
using PM.Application.Common.Specifications.ProjectSpecifications;
using PM.Domain.Entities;

namespace PM.Application.Common.Extensions;

/// <summary>
/// Extensions for filtering and sorting project queries.
/// </summary>
public static class ProjectQueryExtensions
{
    /// <summary>
    /// Filters a queryable of projects based on the provided <paramref name="filter"/>.
    /// </summary>
    /// <param name="projects">The source queryable of projects.</param>
    /// <param name="filter">The project filter to apply.</param>
    /// <returns>A filtered queryable of projects.</returns>
    public static IQueryable<Project> Filter(
       this IQueryable<Project> projects,
       ProjectFilter filter)
    {
        return projects
            .Where(new ProjectDateSpecification(filter.StartDate, filter.EndDate).ToExpression())
            .Where(new ProjectPrioritySpecification(filter.Priority).ToExpression());
    }

    /// <summary>
    /// Sorts a queryable of projects based on the provided <paramref name="sortBy"/> string.
    /// </summary>
    /// <param name="projectsQuery">The source queryable of projects to be sorted.</param>
    /// <param name="sortBy">A comma-separated string specifying sorting properties and directions.</param>
    /// <returns>A sorted queryable of projects.</returns>
    public static IQueryable<Project> Sort(
        this IQueryable<Project> projectsQuery,
        string? sortBy)
    {
        if (string.IsNullOrEmpty(sortBy)) return projectsQuery;

        var sortPairs = sortBy.Split(',', StringSplitOptions.RemoveEmptyEntries);

        var sortedProjectsQuery = projectsQuery.OrderBy(p => 0);

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
                    sortedProjectsQuery = sortOrder == SortStates.Descending
                        ? sortedProjectsQuery.ThenByDescending(p => p.Priority)
                        : sortedProjectsQuery.ThenBy(p => p.Priority);
                    break;

                case "name":
                    sortedProjectsQuery = sortOrder == SortStates.Descending
                        ? sortedProjectsQuery.ThenByDescending(p => p.Name)
                        : sortedProjectsQuery.ThenBy(p => p.Name);
                    break;

                case "startDate":
                    sortedProjectsQuery = sortOrder == SortStates.Descending
                       ? sortedProjectsQuery.ThenByDescending(p => p.StartDate)
                       : sortedProjectsQuery.ThenBy(p => p.StartDate);
                    break;

                case "endDate":
                    sortedProjectsQuery = sortOrder == SortStates.Descending
                       ? sortedProjectsQuery.ThenByDescending(p => p.EndDate)
                       : sortedProjectsQuery.ThenBy(p => p.EndDate);
                    break;

                case "lastName":
                    sortedProjectsQuery = sortOrder == SortStates.Descending
                       ? sortedProjectsQuery.ThenByDescending(p => p.Manager.LastName)
                       : sortedProjectsQuery.ThenBy(p => p.Manager.LastName);
                    break;

                case "firstName":
                    sortedProjectsQuery = sortOrder == SortStates.Descending
                       ? sortedProjectsQuery.ThenByDescending(p => p.Manager.FirstName)
                       : sortedProjectsQuery.ThenBy(p => p.Manager.FirstName);
                    break;

                case "executorCompany":
                    sortedProjectsQuery = sortOrder == SortStates.Descending
                       ? sortedProjectsQuery.ThenByDescending(p => p.ExecutorCompany)
                       : sortedProjectsQuery.ThenBy(p => p.ExecutorCompany);
                    break;

                case "customerCompany":
                    sortedProjectsQuery = sortOrder == SortStates.Descending
                       ? sortedProjectsQuery.ThenByDescending(p => p.CustomerCompany)
                       : sortedProjectsQuery.ThenBy(p => p.CustomerCompany);
                    break;

                default:
                    break;
            }
        }

        return sortedProjectsQuery;
    }
}
