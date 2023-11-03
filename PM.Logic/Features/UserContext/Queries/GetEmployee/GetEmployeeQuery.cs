using ErrorOr;
using MediatR;
using PM.Application.Common.Models.Employee;

namespace PM.Application.Features.EmployeeContext.Queries.GetEmployee;

public sealed record GetEmployeeQuery : IRequest<ErrorOr<List<UserResult>>>;
