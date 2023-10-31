namespace PM.Contracts.EmployeeContracts.Responses;

public sealed record GetEmployeeResponse(
    int Id,
    string FirstName,
    string LastName,
    string? MiddelName,
    string Email);
