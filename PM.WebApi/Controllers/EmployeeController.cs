using Microsoft.AspNetCore.Mvc;
using PM.Application.Features.EmployeeContext.Commands.CreateEmployee;
using PM.Application.Features.EmployeeContext.Commands.DeleteEmployee;
using PM.Application.Features.EmployeeContext.Commands.UpdateEmployee;
using PM.Application.Features.EmployeeContext.Dtos;
using PM.Application.Features.EmployeeContext.Queries.GetEmployee;

namespace PM.WebApi.Controllers;

public class EmployeeController : ApiBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateEmployeeResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateEmployeeAsync(
        CreateEmployeeCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
           result => Ok(result),
           errors => Problem(errors));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetEmployeeResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEmployeeAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var query = new GetEmployeeQuery(id);
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
           result => Ok(result),
           errors => Problem(errors));
    }

    [HttpPut]
    [ProducesResponseType(typeof(UpdateEmployeeResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateEmployeeAsync(
        UpdateEmployeeCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
           result => Ok(result),
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
