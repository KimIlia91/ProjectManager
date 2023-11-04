namespace PM.Application.Features.EmployeeProjectsContext.Dtos;

/// <summary>
/// Represents the result of removing an employee from a project.
/// </summary>
/// <param name="EmployeeId">The ID of the employee that was removed from the project.</param>
/// <param name="ProjectId">The ID of the project from which the employee was removed.</param>
public sealed record RemoveEmployeeFromProjectResult(
    int EmployeeId);