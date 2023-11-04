using ErrorOr;
using MediatR;
using PM.Application.Common.Models.Employee;

namespace PM.Application.Features.EmployeeContext.Queries.GetEmployee;

/// <summary>
/// Represents a query to retrieve a list of employee results.
/// </summary>
public sealed record GetEmployeeListQuery : IRequest<ErrorOr<List<UserResult>>>;