using ErrorOr;
using MediatR;
using PM.Application.Features.AuthContext.Dtos;

namespace PM.Application.Features.AuthContext.Commands.Login;

public sealed record LoginCommand(
    string Email,
    string Password) : IRequest<ErrorOr<LoginResult>>;
