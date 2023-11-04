using Mapster;

namespace PM.Application.Features.EmployeeContext.Dtos;

/// <summary>
/// Represents the result of a user retrieval operation.
/// </summary>
/// <param name="Id">The unique identifier of the user.</param>
/// <param name="FirstName">The first name of the user.</param>
/// <param name="LastName">The last name of the user.</param>
/// <param name="MiddleName">The middle name of the user (if available).</param>
/// <param name="Email">The email address of the user.</param>
public sealed record GetUserResult(
     int Id,
     string FirstName,
     string LastName,
     string? MiddleName,
     string Email);