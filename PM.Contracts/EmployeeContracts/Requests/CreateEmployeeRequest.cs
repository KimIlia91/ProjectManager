namespace PM.Contracts.EmployeeContracts.Requests;

public record CreateEmployeeRequest(
    string FirstName,
    string LastName,
    string? MiddelName,
    string Email);
