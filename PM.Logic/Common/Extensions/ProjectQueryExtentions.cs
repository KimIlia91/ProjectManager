using PM.Application.Common.Enums;
using PM.Application.Common.Specifications.ProjectSpecifications;
using PM.Application.Features.ProjectContext.Queries.GetProjectList;
using PM.Domain.Entities;

namespace PM.Application.Common.Extensions;

public static class ProjectQueryExtentions
{
    public static IQueryable<Project> Where(
       this IQueryable<Project> projects,
       ProjectFilter filter)
    {
        return projects
            .Where(new ProjectDateSpecification(filter.StartDate, filter.EndDate).ToExpression())
            .Where(new ProjectManagerSpecification(filter.ManagerId).ToExpression())
            .Where(new ProjectPrioretySpecification(filter.Priority).ToExpression());
    }

    public static IQueryable<Project> SortProject(
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
                case "Priority":
                    sortedProjectsQuery = sortOrder == SortStates.Descending
                        ? sortedProjectsQuery.ThenByDescending(p => p.Priority)
                        : sortedProjectsQuery.ThenBy(p => p.Priority);
                    break;

                case "Name":
                    sortedProjectsQuery = sortOrder == SortStates.Descending
                        ? sortedProjectsQuery.ThenByDescending(p => p.Name)
                        : sortedProjectsQuery.ThenBy(p => p.Name);
                    break;

                case "StartDate":
                    sortedProjectsQuery = sortOrder == SortStates.Descending
                       ? sortedProjectsQuery.ThenByDescending(p => p.StartDate)
                       : sortedProjectsQuery.ThenBy(p => p.StartDate);
                    break;

                case "EndDate":
                    sortedProjectsQuery = sortOrder == SortStates.Descending
                       ? sortedProjectsQuery.ThenByDescending(p => p.EndDate)
                       : sortedProjectsQuery.ThenBy(p => p.EndDate);
                    break;

                case "ManagerId":
                    sortedProjectsQuery = sortOrder == SortStates.Descending
                       ? sortedProjectsQuery.ThenByDescending(p => p.Manager.Id)
                       : sortedProjectsQuery.ThenBy(p => p.Manager.Id);
                    break;

                case "ExecutorCompany":
                    sortedProjectsQuery = sortOrder == SortStates.Descending
                       ? sortedProjectsQuery.ThenByDescending(p => p.ExecutorCompany)
                       : sortedProjectsQuery.ThenBy(p => p.ExecutorCompany);
                    break;

                case "CustomerCompany":
                    sortedProjectsQuery = sortOrder == SortStates.Descending
                       ? sortedProjectsQuery.ThenByDescending(p => p.CustomerCompany)
                       : sortedProjectsQuery.ThenBy(p => p.CustomerCompany);
                    break;

                default:
                    break;
            }
        }

        return sortedProjectsQuery.AsQueryable();
    }
}
