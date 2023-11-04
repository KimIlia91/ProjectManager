using ErrorOr;
using MediatR;
using PM.Application.Features.EmployeeContext.Dtos;

namespace PM.Application.Features.UserContext.Commands.CreateUser;

/// <summary>
/// Represents a command to create a new user with the specified details.
/// </summary>
/// <param name="FirstName">First name of the user.</param>
/// <param name="LastName">Last name of the user.</param>
/// <param name="MiddelName">Middle name of the user (optional).</param>
/// <param name="Email">Email address of the user.</param>
/// <param name="Password">Password for the user.</param>
/// <param name="RoleName">Role name associated with the user.</param>
public record CreateUserCommand(
    string FirstName,
    string LastName,
    string? MiddelName,
    string Email,
    string Password,
    string RoleName) : IRequest<ErrorOr<CreateUserResult>>;
