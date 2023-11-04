namespace PM.Application.Features.EmployeeContext.Dtos;

/// <summary>
/// Represents the result of a user creation operation.
/// </summary>
/// <param name="UserId">The ID of the newly created user.</param>
public record CreateUserResult(int UserId);

