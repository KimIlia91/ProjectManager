using ErrorOr;
using MediatR;
using PM.Application.Features.EmployeeContext.Dtos;

namespace PM.Application.Features.EmployeeContext.Queries.GetEmployee;

/// <summary>
/// Represents a query to retrieve information about a specific user by their ID.
/// </summary>
/// <param name="UserId">User ID</param>
public sealed record GetUserQuery(
    int UserId) : IRequest<ErrorOr<GetUserResult>>;
