namespace PM.Contracts.EmployeeContracts.Responses;

public sealed record GetEmployeeResponse(
    string FirstName,
    string LastName,
    string? MiddelName,
    string Email);
