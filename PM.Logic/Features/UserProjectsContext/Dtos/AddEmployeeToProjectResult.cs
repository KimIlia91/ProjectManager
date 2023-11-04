namespace PM.Application.Features.EmployeeProjectsContext.Dtos;

/// <summary>
/// Represents the result of adding an employee to a project.
/// </summary>
/// <param name="EmployeeId">The ID of the employee that was added to the project.</param>
public sealed record AddEmployeeToProjectResult(int EmployeeId);