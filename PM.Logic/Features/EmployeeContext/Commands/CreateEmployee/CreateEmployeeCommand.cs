using ErrorOr;
using MediatR;
using PM.Application.Features.EmployeeContext.Dtos;

namespace PM.Application.Features.EmployeeContext.Commands.CreateEmployee;

public record CreateEmployeeCommand(
    string FirstName,
    string LastName,
    string? MiddelName,
    string Email,
    string Password,
    string RoleName) : IRequest<ErrorOr<CreateEmployeeResult>>;
