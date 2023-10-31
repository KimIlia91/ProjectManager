using Microsoft.AspNetCore.Mvc;
using PM.Application.Features.TaskContext.Commands.CreateTask;
using PM.Application.Features.TaskContext.Commands.UpdateTask;
using PM.Application.Features.TaskContext.Queries.GetTask;
using PM.Contracts.TaskContracts.Requests;
using PM.Contracts.TaskContracts.Responses;

namespace PM.WebApi.Controllers;

public class TaskController : BaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateTaskResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateTaskAsync(
        CreateTaskRequest request,
        CancellationToken cancellationToken)
    {
        var command = Mapper.Map<CreateTaskCommand>(request);
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
            result => Ok(Mapper.Map<CreateTaskResponse>(result)),
            errors => Problem(errors));
    }


    [HttpPut]
    [ProducesResponseType(typeof(UpdateTaskResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateTaskAsync(
        UpdateTaskRequest request,
        CancellationToken cancellationToken)
    {
        var command = Mapper.Map<UpdateTaskCommand>(request);
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
            result => Ok(Mapper.Map<UpdateTaskResponse>(result)),
            errors => Problem(errors));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetTaskResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateTaskAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var query = new GetTaskQuery(id);
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
            result => Ok(Mapper.Map<GetTaskResponse>(result)),
            errors => Problem(errors));
    }
}
