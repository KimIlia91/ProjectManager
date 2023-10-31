using Microsoft.AspNetCore.Mvc;
using PM.Application.Features.ProjectContext.Commands.CreateProject;
using PM.Application.Features.ProjectContext.Commands.DeleteProject;
using PM.Application.Features.ProjectContext.Commands.UpdateProject;
using PM.Application.Features.ProjectContext.Queries.GetProject;
using PM.Contracts.ProjectContracts.Requests;
using PM.Contracts.ProjectContracts.Responses;

namespace PM.WebApi.Controllers;

public class ProjectController : BaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateProjectResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateProjectAsync(
        CreateProjectRequest request, CancellationToken cancellationToken)
    {
        var command = Mapper.Map<CreateProjectCommand>(request);
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
            result => Ok(Mapper.Map<CreateProjectResponse>(result)),
            errors => Problem(errors));
    }

    [HttpPut]
    [ProducesResponseType(typeof(UpdateProjectResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateProjectAsync(
        UpdateProjectRequest request, CancellationToken cancellationToken)
    {
        var command = Mapper.Map<UpdateProjectCommand>(request);
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
            result => Ok(Mapper.Map<UpdateProjectResponse>(result)),
            errors => Problem(errors));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetProjectResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProjectAsync(
        int id, CancellationToken cancellationToken)
    {
        var query = new GetProjectQuery(id);
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
            result => Ok(Mapper.Map<GetProjectResponse>(result)),
            errors => Problem(errors));
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(GetProjectResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteProjectAsync(
        int id, CancellationToken cancellationToken)
    {
        var command = new DeleteProjectCommand() { Id = id };
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
            result => Ok(Mapper.Map<DeleteProjectResponse>(result)),
            errors => Problem(errors));
    }
}