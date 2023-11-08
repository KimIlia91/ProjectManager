using ErrorOr;
using MediatR;
using PM.Application.Features.AuthContext.Dtos;

namespace PM.Application.Features.AuthContext.Commands.RefreshAccessToken;

/// <summary>
/// Represents a command to refresh an access token using a refresh token.
/// </summary>
/// <param name="RefreshToken">The refresh token used to obtain a new access token.</param>
public sealed record RefreshAccessTokenCommand(string RefreshToken) 
    : IRequest<ErrorOr<AuthResult>>;

