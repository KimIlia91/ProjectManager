namespace PM.Application.Features.EmployeeProjectsContext.Dtos;

/// <summary>
/// Represents the result of removing an employee from a project.
/// </summary>
/// <param name="UserId">The ID of the user that was removed from the project.</param>
public sealed record RemoveUserFromProjectResult(
    int UserId);