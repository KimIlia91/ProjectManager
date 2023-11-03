using ErrorOr;
using MediatR;
using PM.Application.Features.AuthContext.Dtos;

namespace PM.Application.Features.AuthContext.Commands.RefreshAccessToken;

public sealed record RefreshAccessTokenCommand(
    string RefreshToken) : IRequest<ErrorOr<LoginResult>>;
