namespace PM.Contracts.EmployeeContracts.Requests;

/// <summary>
/// Запрос на обновление данных о сотруднике
/// </summary>
public sealed record UpdateEmployeeRequest(
    int Id,
    string FirstName,
    string LastName,
    string? MiddelName,
    string Email,
    string RoleName);

