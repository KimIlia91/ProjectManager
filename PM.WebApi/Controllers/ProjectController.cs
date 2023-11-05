using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.Application.Common.Constants;
using PM.Application.Features.ProjectContext.Commands.CreateProject;
using PM.Application.Features.ProjectContext.Commands.DeleteProject;
using PM.Application.Features.ProjectContext.Commands.UpdateProject;
using PM.Application.Features.ProjectContext.Dtos;
using PM.Application.Features.ProjectContext.Queries.GetCurrentUserProjectList;
using PM.Application.Features.ProjectContext.Queries.GetProject;
using PM.Application.Features.ProjectContext.Queries.GetProjectList;
using PM.Domain.Common.Constants;

namespace PM.WebApi.Controllers;

/// <summary>
/// Represents a controller for managing projects.
/// </summary>
public class ProjectController : ApiBaseController
{
    /// <summary>
    /// Create a new project.
    /// </summary>
    /// <param name="command">The command for creating the project.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult representing the operation result, which can be one of the following:
    /// - 200 OK with the created project if successful.
    /// - A problem response with errors if the operation encounters issues.
    /// </returns>
    [HttpPost]
    [Authorize(Roles = RoleConstants.Supervisor)]
    [ProducesResponseType(typeof(CreateProjectResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateProjectAsync(
        CreateProjectCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    /// <summary>
    /// Update an existing project.
    /// </summary>
    /// <param name="command">The command for updating the project.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult representing the operation result, which can be one of the following:
    /// - 200 OK with the updated project if successful.
    /// - A problem response with errors if the operation encounters issues.
    /// </returns>
    [HttpPut]
    [Authorize(Roles = RoleConstants.Supervisor)]
    [ProducesResponseType(typeof(UpdateProjectResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateProjectAsync(
        UpdateProjectCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    /// <summary>
    /// Retrieve a project by its ID.
    /// </summary>
    /// <param name="id">The ID of the project to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult representing the retrieved project if found.
    /// - 200 OK with the project details if successful.
    /// - A problem response with errors if the project is not found or if there are issues.
    /// </returns>
    [HttpGet("{id}")]
    [Authorize(Policy = PolicyConstants.ProjectManagerPolicy)]
    [ProducesResponseType(typeof(GetProjectResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProjectAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var query = new GetProjectQuery(id);
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    /// <summary>
    /// Delete a project by its ID.
    /// </summary>
    /// <param name="id">The ID of the project to delete.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult indicating the result of the deletion operation, which is:
    /// - 204 No Content: If the project is successfully deleted.
    /// - A problem response with errors if the project is not found or if there are issues.
    /// </returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = RoleConstants.Supervisor)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteProjectAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteProjectCommand() { Id = id };
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
            result => NoContent(),
            errors => Problem(errors));
    }

    /// <summary>
    /// Retrieve a list of projects based on query parameters.
    /// </summary>
    /// <param name="query">The query parameters for filtering and sorting projects.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult representing the list of projects matching the query.
    /// - 200 OK with the list of projects if successful.
    /// - A problem response with errors if there are issues.
    /// </returns>
    [HttpGet]
    [Authorize(Policy = PolicyConstants.ProjectManagerPolicy)]
    [ProducesResponseType(typeof(List<GetProjectListResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProjectListAsync(
        [FromQuery] GetProjectListQuery query,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    /// <summary>
    /// Retrieve a list of projects associated with the authenticated user.
    /// </summary>
    /// <param name="query">The query parameters for filtering and sorting user-specific projects.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult representing the list of user-specific projects matching the query.
    /// - 200 OK with the list of projects if successful.
    /// - A problem response with errors if there are issues.
    /// </returns>
    [HttpGet("User")]
    [ProducesResponseType(typeof(List<GetProjectListResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCurrentUserProjectListAsync(
        [FromQuery] GetCurrentUserProjectListQuery query,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }
}