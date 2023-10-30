using ErrorOr;
using MediatR;
using PM.Application.Features.EmployeeContext.Dtos;

namespace PM.Application.Features.EmployeeContext.Commands.DeleteEmployee;

public sealed record DeleteEmployeeCommand(
    int EmployeeId) : IRequest<ErrorOr<DeleteEmployeeResult>>;
