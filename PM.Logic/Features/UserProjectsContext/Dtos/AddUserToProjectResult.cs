namespace PM.Application.Features.EmployeeProjectsContext.Dtos;

/// <summary>
/// Represents the result of adding an employee to a project.
/// </summary>
/// <param name="UserId">The ID of the user that was added to the project.</param>
public sealed record AddUserToProjectResult(int UserId);