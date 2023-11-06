using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.Application.Features.EmployeeProjectsContext.Commands.AddEmployeeToProject;
using PM.Application.Features.EmployeeProjectsContext.Commands.RemoveEmployeeFromProject;
using PM.Application.Features.EmployeeProjectsContext.Dtos;
using PM.Domain.Common.Constants;

namespace PM.WebApi.Controllers;

/// <summary>
/// Controller for managing user-to-project assignments.
/// </summary>
public class UserProjectsController : ApiBaseController
{
    /// <summary>
    /// Add an employee to a project.
    /// </summary>
    /// <param name="command">The command for adding an employee to a project.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult representing the operation result, which can be one of the following:
    /// - 200 OK with the result of adding the employee to the project if successful.
    /// - A problem response with errors if the operation encounters issues.
    /// </returns>
    [HttpPost]
    [Authorize(Roles = $"{RoleConstants.Manager}, {RoleConstants.Supervisor}")]
    [ProducesResponseType(typeof(AddUserToProjectResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddEmployeeToProjectAsync(
        AddUserToProjectCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    /// <summary>
    /// Remove an employee from a project.
    /// </summary>
    /// <param name="projectId">The ID of the project from which to remove the employee.</param>
    /// <param name="employeeId">The ID of the employee to be removed from the project.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult indicating the result of the removal operation, which is:
    /// - 204 No Content: If the employee is successfully removed from the project.
    /// - A problem response with errors if there are issues.
    /// </returns>
    [HttpDelete("{projectId}/{employeeId}")]
    [Authorize(Roles = $"{RoleConstants.Supervisor}, {RoleConstants.Manager}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RemoveEmployeeFromProjectAsync(
        int projectId,
        int employeeId,
        CancellationToken cancellationToken)
    {
        var command = new RemoveUserFromProjectCommand()
        {
            ProjectId = projectId,
            UserId = employeeId
        };

        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
            result => NoContent(),
            errors => Problem(errors));
    }
}

