using ErrorOr;
using MediatR;
using PM.Application.Features.EmployeeContext.Dtos;

namespace PM.Application.Features.EmployeeContext.Commands.UpdateEmployee;

public sealed record UpdateEmployeeCommand(
    int Id,
    string FirstName,
    string LastName,
    string? MiddelName,
    string Email) : IRequest<ErrorOr<UpdateEmployeeResult>>;
