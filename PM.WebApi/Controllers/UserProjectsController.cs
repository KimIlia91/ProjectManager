using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.Application.Features.EmployeeProjectsContext.Commands.AddEmployeeToProject;
using PM.Application.Features.EmployeeProjectsContext.Commands.RemoveEmployeeFromProject;
using PM.Application.Features.EmployeeProjectsContext.Dtos;
using PM.Domain.Common.Constants;

namespace PM.WebApi.Controllers;

/// <summary>
/// 
/// </summary>
public class UserProjectsController : ApiBaseController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = $"{RoleConstants.Manager}, {RoleConstants.Supervisor}")]
    [ProducesResponseType(typeof(AddEmployeeToProjectResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddEmployeeToProjectAsync(
        AddEmployeeToProjectCommand command,
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
    /// <param name="projectId"></param>
    /// <param name="employeeId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{projectId}/{employeeId}")]
    [Authorize(Roles = $"{RoleConstants.Supervisor}, {RoleConstants.Manager}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RemoveEmployeeFromProjectAsync(
        int projectId,
        int employeeId,
        CancellationToken cancellationToken)
    {
        var command = new RemoveEmployeeFromProjectCommand()
        {
            ProjectId = projectId,
            EmployeeId = employeeId
        };

        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
            result => NoContent(),
            errors => Problem(errors));
    }
}
