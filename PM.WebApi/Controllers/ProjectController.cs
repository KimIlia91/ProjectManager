using Microsoft.AspNetCore.Mvc;
using PM.Application.Features.ProjectContext.Commands.CreateProject;
using PM.Application.Features.ProjectContext.Commands.DeleteProject;
using PM.Application.Features.ProjectContext.Commands.UpdateProject;
using PM.Application.Features.ProjectContext.Dtos;
using PM.Application.Features.ProjectContext.Queries.GetProject;
using PM.Application.Features.ProjectContext.Queries.GetProjectList;

namespace PM.WebApi.Controllers;

public class ProjectController : BaseController
{
    [HttpPost]
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

    [HttpPut]
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


    [HttpDelete("{id}")]
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

    [HttpGet]
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
}