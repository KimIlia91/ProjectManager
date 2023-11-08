using MediatR;

namespace PM.Application.Features.AuthContext.Commands.Logout;

/// <summary>
/// Represents a command to log a user out.
/// </summary>
public sealed record LogoutCommand : IRequest;
