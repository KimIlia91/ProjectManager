using ErrorOr;
using MediatR;
using PM.Application.Features.EmployeeContext.Dtos;

namespace PM.Application.Features.EmployeeContext.Queries.GetEmployee;

public sealed record GetUserQuery(
    int EmployeeId) : IRequest<ErrorOr<GetUserResult>>;
