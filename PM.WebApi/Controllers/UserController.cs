using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.Application.Features.EmployeeContext.Dtos;
using PM.Application.Features.EmployeeContext.Queries.GetEmployee;
using PM.Application.Features.UserContext.Commands.CreateUser;
using PM.Application.Features.UserContext.Commands.DeleteUser;
using PM.Application.Features.UserContext.Commands.UpdateUser;
using PM.Application.Features.UserContext.Queries.GetUserList;
using PM.Application.Features.UserContext.Queries.GetUsersOfProject;
using PM.Domain.Common.Constants;

namespace PM.WebApi.Controllers;

/// <summary>
/// Controller for managing users.
/// </summary>
public class UserController : ApiBaseController
{
    /// <summary>
    /// Create a new user.
    /// </summary>
    /// <param name="command">The command for creating the user.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult representing the operation result, which can be one of the following:
    /// - 200 OK with the created user if successful.
    /// - A problem response with errors if the operation encounters issues.
    /// </returns>
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
    /// Retrieve a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult representing the retrieved user if found.
    /// - 200 OK with the user details if successful.
    /// - A problem response with errors if the user is not found or if there are issues.
    /// </returns>
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
    /// Update an existing user.
    /// </summary>
    /// <param name="command">The command for updating the user.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult representing the operation result, which can be one of the following:
    /// - 200 OK with the updated user if successful.
    /// - A problem response with errors if the operation encounters issues.
    /// </returns>
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
    /// Delete a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult indicating the result of the deletion operation, which is:
    /// - 204 No Content: If the user is successfully deleted.
    /// - A problem response with errors if the user is not found or if there are issues.
    /// </returns>
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
    /// Retrieve a list of users based on query parameters.
    /// </summary>
    /// <param name="query">The query parameters for filtering and sorting users.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult representing the list of users matching the query.
    /// - 200 OK with the list of users if successful.
    /// - A problem response with errors if there are issues.
    /// </returns>
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

    /// <summary>
    /// Retrieve a list of users assigned to a specific project.
    /// </summary>
    /// <param name="projectId">The ID of the project to retrieve users for.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult representing the list of users assigned to the project.
    /// - 200 OK with the list of users if successful.
    /// - A problem response with errors if there are issues.
    /// </returns>
    [HttpGet("projectEmployees/{projectId}")]
    [ProducesResponseType(typeof(UpdateUserResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProjectUserListAsync(
       int projectId,
       CancellationToken cancellationToken)
    {
        var query = new GetUsersOfProjectQuery(projectId);
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
          result => Ok(result),
          errors => Problem(errors));
    }
}
