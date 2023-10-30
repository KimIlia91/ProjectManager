using ErrorOr;
using MediatR;
using PM.Application.Features.EmployeeContext.Dtos;

namespace PM.Application.Features.EmployeeContext.Queries.GetEmployees;

public sealed record GetEmployeesQuery()
    : IRequest<ErrorOr<List<GetEmployeeResult>>>;