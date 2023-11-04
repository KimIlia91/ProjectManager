using Microsoft.AspNetCore.Authorization;
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
using PM.Domain.Common.Constants;

namespace PM.WebApi.Controllers;

/// <summary>
/// 
/// </summary>
public class UserController : ApiBaseController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = $"{RoleConstants.Supervisor}")]
    [ProducesResponseType(typeof(CreateUserResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateUserAsync(
        CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
           result => Ok(result),
           errors => Problem(errors));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetUserResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var query = new GetUserQuery(id);
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
           result => Ok(result),
           errors => Problem(errors));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize(Roles = $"{RoleConstants.Supervisor}")]
    [ProducesResponseType(typeof(UpdateUserResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateUserAsync(
        UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
           result => Ok(result),
           errors => Problem(errors));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = $"{RoleConstants.Supervisor}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteUserAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand { UserId = id };
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
           result => NoContent(),
           errors => Problem(errors));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("projectEmployees/{projectId}")]
    [ProducesResponseType(typeof(UpdateUserResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProjectUserListAsync(
       int projectId,
       CancellationToken cancellationToken)
    {
        var query = new GetProjectUserListQuery(projectId);
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
          result => Ok(result),
          errors => Problem(errors));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("Managers")]
    [Authorize(Roles = RoleConstants.Supervisor)]
    [ProducesResponseType(typeof(List<UserResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetManagerListAsync(
        CancellationToken cancellationToken)
    {
        var query = new GetManagerListQuery();
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
          result => Ok(result),
          errors => Problem(errors));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("Employees")]
    [Authorize(Roles = $"{RoleConstants.Supervisor}, {RoleConstants.Manager}")]
    [ProducesResponseType(typeof(List<UserResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEmployeeListAsync(
        CancellationToken cancellationToken)
    {
        var query = new GetEmployeeListQuery();
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
          result => Ok(result),
          errors => Problem(errors));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = RoleConstants.Supervisor)]
    [ProducesResponseType(typeof(List<GetUserResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserListAsync(
       CancellationToken cancellationToken)
    {
        var query = new GetUserListQuery();
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
          result => Ok(result),
          errors => Problem(errors));
    }
}
