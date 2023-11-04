using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.Application.Features.ProjectContext.Commands.CreateProject;
using PM.Application.Features.ProjectContext.Commands.DeleteProject;
using PM.Application.Features.ProjectContext.Commands.UpdateProject;
using PM.Application.Features.ProjectContext.Dtos;
using PM.Application.Features.ProjectContext.Queries.GetProject;
using PM.Application.Features.ProjectContext.Queries.GetProjectList;
using PM.Application.Features.ProjectContext.Queries.GetUserProjectList;
using PM.Domain.Common.Constants;

namespace PM.WebApi.Controllers;

/// <summary>
/// Represents a controller for managing projects.
/// </summary>
public class ProjectController : ApiBaseController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
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
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = RoleConstants.Supervisor)]
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
    /// 
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("User")]
    [ProducesResponseType(typeof(List<GetProjectListResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserProjectListAsync(
        [FromQuery] GetUserProjectListQuery query,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }
}