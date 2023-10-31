using ErrorOr;
using MediatR;
using PM.Application.Features.UserContext.Dtos;

namespace PM.Application.Features.UserContext.Commands.Register;

public sealed record RegisterCommand(
    string Email,
    string PhoneNumber,
    string RoleName,
    string Password,
    string ConfirmPassword) : IRequest<ErrorOr<RegisterResult>>;
