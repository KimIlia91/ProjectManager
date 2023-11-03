using Microsoft.AspNetCore.Mvc;
using PM.Application.Common.Models.Employee;
using PM.Application.Features.EmployeeContext.Dtos;
using PM.Application.Features.EmployeeContext.Queries.GetEmployee;
using PM.Application.Features.EmployeeContext.Queries.GetEmployees;
using PM.Application.Features.EmployeeContext.Queries.GetManagers;
using PM.Application.Features.EmployeeContext.Queries.GetProjectEmployees;
using PM.Application.Features.UserContext.Commands.CreateUser;
using PM.Application.Features.UserContext.Commands.DeleteUser;
using PM.Application.Features.UserContext.Commands.UpdateUser;

namespace PM.WebApi.Controllers;

public class UserController : ApiBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateUserResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateEmployeeAsync(
        CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
           result => Ok(result),
           errors => Problem(errors));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetUserResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEmployeeAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var query = new GetUserQuery(id);
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
           result => Ok(result),
           errors => Problem(errors));
    }

    [HttpPut]
    [ProducesResponseType(typeof(UpdateUserResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateEmployeeAsync(
        UpdateUserCommand command,
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
        var command = new DeleteUserCommand { EmployeeId = id };
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
           result => NoContent(),
           errors => Problem(errors));
    }

    [HttpGet("projectEmployees/{projectId}")]
    [ProducesResponseType(typeof(UpdateUserResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProjectEmployeesAsync(
       int projectId,
       CancellationToken cancellationToken)
    {
        var query = new GetProjectEmployeesQuery(projectId);
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
          result => Ok(result),
          errors => Problem(errors));
    }

    [HttpGet("Managers")]
    [ProducesResponseType(typeof(List<UserResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetManagerListAsync(
        CancellationToken cancellationToken)
    {
        var query = new GetMenagersQuery();
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
          result => Ok(result),
          errors => Problem(errors));
    }

    [HttpGet("Employees")]
    [ProducesResponseType(typeof(List<UserResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEmployeeListAsync(
        CancellationToken cancellationToken)
    {
        var query = new GetEmployeeQuery();
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
          result => Ok(result),
          errors => Problem(errors));
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<GetUserResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsersListAsync(
       CancellationToken cancellationToken)
    {
        var query = new GetUsersQuery();
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
          result => Ok(result),
          errors => Problem(errors));
    }
}
