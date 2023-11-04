using ErrorOr;
using MediatR;
using PM.Application.Common.Models.Employee;

namespace PM.Application.Features.EmployeeContext.Queries.GetManagers;

/// <summary>
/// Represents a query to retrieve a list of manager results.
/// </summary>
public sealed record GetManagerListQuery : IRequest<ErrorOr<List<UserResult>>>;
