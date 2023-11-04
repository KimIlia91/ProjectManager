using Mapster;
using PM.Domain.Entities;

namespace PM.Application.Features.EmployeeContext.Dtos;

/// <summary>
/// Represents the result of a user update operation.
/// </summary>
public sealed class UpdateUserResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the middle name of the user (if available).
    /// </summary>
    public string? MiddleName { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public string Email { get; set; } = null!;
}