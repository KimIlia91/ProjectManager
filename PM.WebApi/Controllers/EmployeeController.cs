using Microsoft.AspNetCore.Mvc;
using PM.Application.Features.EmployeeContext.Commands.CreateEmployee;
using PM.Application.Features.EmployeeContext.Queries.GetEmployee;
using PM.Contracts.EmployeeContracts.Requests;
using PM.Contracts.EmployeeContracts.Responses;

namespace PM.WebApi.Controllers;

public class EmployeeController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateEmployeeAsync(
        CreateEmployeeRequest request,
        CancellationToken cancellationToken)
    {
        var command = Mapper.Map<CreateEmployeeCommand>(request);
        var result = await Mediator.Send(command, cancellationToken);
        return result.Match(
           result => Ok(Mapper.Map<CreateEmployeeResponse>(result)),
           errors => Problem(errors));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var query = new GetEmployeeQuery(id);
        var result = await Mediator.Send(query, cancellationToken);
        return result.Match(
           result => Ok(Mapper.Map<GetEmployeeResponse>(result)),
           errors => Problem(errors));
    }
}
