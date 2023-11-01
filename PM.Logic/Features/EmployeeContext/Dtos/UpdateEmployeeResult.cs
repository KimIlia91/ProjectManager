namespace PM.Application.Features.EmployeeContext.Dtos;

public record UpdateEmployeeResult(
     int Id,
     string FirstName,
     string LastName,
     string? MiddelName,
     string Email);

public sealed class UpdateEmployeeResult
{

}