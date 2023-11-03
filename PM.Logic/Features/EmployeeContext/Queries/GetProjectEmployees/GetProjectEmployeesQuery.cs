using ErrorOr;
using MediatR;
using PM.Application.Common.Models.Employee;

namespace PM.Application.Features.EmployeeContext.Queries.GetProjectEmployees
{
    public sealed record GetProjectEmployeesQuery(
        int ProjctId) : IRequest<ErrorOr<List<EmployeeResult>>>;
}
