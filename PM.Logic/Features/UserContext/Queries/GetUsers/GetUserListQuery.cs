using ErrorOr;
using MediatR;
using PM.Application.Features.EmployeeContext.Dtos;

namespace PM.Application.Features.EmployeeContext.Queries.GetEmployees;

/// <summary>
/// Represents a query to retrieve a list of user information.
/// </summary>
public sealed record GetUserListQuery 
    : IRequest<ErrorOr<List<GetUserResult>>>;
