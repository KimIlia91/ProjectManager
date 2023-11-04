using ErrorOr;
using MediatR;
using PM.Application.Features.EmployeeContext.Dtos;
using PM.Domain.Entities;
using System.Text.Json.Serialization;

namespace PM.Application.Features.UserContext.Commands.UpdateUser;

/// <summary>
/// Represents a command to update user information.
/// </summary>
public sealed class UpdateUserCommand
    : IRequest<ErrorOr<UpdateUserResult>>
{
    /// <summary>
    /// Gets or sets the user entity to be updated. Use [JsonIgnore] to exclude it from serialization.
    /// </summary>
    [JsonIgnore] public User? User { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user to be updated.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the first name of the user. Cannot be null.
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the last name of the user. Cannot be null.
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the middle name of the user.
    /// </summary>
    public string? MiddelName { get; set; }

    /// <summary>
    /// Gets or sets the email of the user. Cannot be null.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Gets or sets the role name associated with the user. Cannot be null.
    /// </summary>
    public string RoleName { get; set; } = null!;
}
