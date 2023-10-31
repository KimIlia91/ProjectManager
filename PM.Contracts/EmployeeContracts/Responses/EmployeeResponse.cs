namespace PM.Contracts.EmployeeContracts.Responses;

public class EmployeeResponse
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string MiddelName { get; set; } = string.Empty;

    public string Email { get; set; } = null!;
}