using ErrorOr;
using MediatR;
using PM.Application.Features.AuthContext.Dtos;

namespace PM.Application.Features.AuthContext.Commands.Login;

/// <summary>
/// Represents a command to authenticate a user by providing email and password.
/// </summary>
/// <param name="Email">The user's email address used for authentication.</param>
/// <param name="Password">The user's password used for authentication.</param>
public sealed record LoginCommand(
    string Email,
    string Password) : IRequest<ErrorOr<AuthResult>>;
