using ErrorOr;
using MediatR;
using PM.Application.Features.UserContext.Dtos;

namespace PM.Application.Features.UserContext.Commands.Register;

public sealed record RegisterCommand(
    string Email,
    string FirstName,
    string LastName,
    string MiddelName,
    int RoleId,
    string Password) : IRequest<ErrorOr<RegisterResult>>;
