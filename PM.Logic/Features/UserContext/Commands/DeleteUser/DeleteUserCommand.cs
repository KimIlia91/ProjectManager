using ErrorOr;
using MediatR;
using PM.Application.Features.EmployeeContext.Dtos;
using PM.Domain.Entities;
using System.Text.Json.Serialization;

namespace PM.Application.Features.UserContext.Commands.DeleteUser;

/// <summary>
/// Represents a command to delete a user.
/// </summary>
public sealed class DeleteUserCommand
    : IRequest<ErrorOr<DeleteUserResult>>
{
    /// <summary>
    /// Gets or sets the user to be deleted.
    /// </summary>
    [JsonIgnore] public User? Employee { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user to be deleted.
    /// </summary>
    public int UserId { get; set; }
}