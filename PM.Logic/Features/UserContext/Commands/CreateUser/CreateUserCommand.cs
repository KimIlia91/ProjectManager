using ErrorOr;
using MediatR;
using PM.Application.Features.EmployeeContext.Dtos;

namespace PM.Application.Features.UserContext.Commands.CreateUser;

public record CreateUserCommand(
    string FirstName,
    string LastName,
    string? MiddelName,
    string Email,
    string Password,
    string RoleName) : IRequest<ErrorOr<CreateUserResult>>;
