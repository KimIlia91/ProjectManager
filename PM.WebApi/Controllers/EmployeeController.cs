using Microsoft.AspNetCore.Mvc;
using PM.Application.Features.EmployeeContext.Commands.CreateEmployee;
using PM.Application.Features.EmployeeContext.Commands.DeleteEmployee;
using PM.Application.Features.EmployeeContext.Commands.UpdateEmployee;
using PM.Application.Features.EmployeeContext.Queries.GetEmployee;
using PM.Contracts.EmployeeContracts.Requests;
using PM.Contracts.EmployeeContracts.Responses;

namespace PM.WebApi.Controllers;

public class EmployeeController : BaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateEmployeeResponse), StatusCodes.Status200OK)]
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
    [ProducesResponseType(typeof(GetEmployeeResponse), StatusCodes.Status200OK)]
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

    [HttpPut]
    [ProducesResponseType(typeof(UpdateEmployeeResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateEmployeeAsync(
        UpdateEmployeeRequest request,
        CancellationToken cancellationToken)
    {
        var command = Mapper.Map<UpdateEmployeeCommand>(request);
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
           result => Ok(Mapper.Map<UpdateEmployeeResponse>(result)),
           errors => Problem(errors));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteEmployeeAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteEmployeeCommand { EmployeeId = id };
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
           result => NoContent(),
           errors => Problem(errors));
    }
}
