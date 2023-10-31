namespace PM.Contracts.EmployeeProjectsContracts.Requests;

public sealed record AddEmployeeToProjectRequest(
    int EmployeeId,
    int ProjectId);
