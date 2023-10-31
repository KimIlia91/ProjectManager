namespace PM.Contracts.EmployeeContracts.Responses;

public sealed record UpdateEmployeeResponse(
    int Id,
    string FirstName,
    string LastName,
    string? MiddelName,
    string Email);
