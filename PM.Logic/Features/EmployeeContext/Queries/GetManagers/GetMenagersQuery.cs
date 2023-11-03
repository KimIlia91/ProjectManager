using ErrorOr;
using MediatR;
using PM.Application.Common.Models.Employee;

namespace PM.Application.Features.EmployeeContext.Queries.GetManagers;

public sealed record GetMenagersQuery 
    : IRequest<ErrorOr<List<EmployeeResult>>>;

