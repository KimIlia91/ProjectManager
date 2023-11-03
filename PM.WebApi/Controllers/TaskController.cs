using Microsoft.AspNetCore.Mvc;
using PM.Application.Common.Models.Task;
using PM.Application.Features.TaskContext.Commands.CreateTask;
using PM.Application.Features.TaskContext.Commands.DeleteTask;
using PM.Application.Features.TaskContext.Commands.UpdateTask;
using PM.Application.Features.TaskContext.Dtos;
using PM.Application.Features.TaskContext.Queries.GetProjectTasks;
using PM.Application.Features.TaskContext.Queries.GetTask;
using PM.Application.Features.TaskContext.Queries.GetTaskList;

namespace PM.WebApi.Controllers;

public class TaskController : ApiBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateTaskResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateTaskAsync(
        CreateTaskCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }


    [HttpPut]
    [ProducesResponseType(typeof(UpdateTaskResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateTaskAsync(
        UpdateTaskCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetTaskResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTaskAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var query = new GetTaskQuery(id);
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteTaskAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteTaskCommand { Id = id };
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
            result => NoContent(),
            errors => Problem(errors));
    }


    [HttpGet]
    [ProducesResponseType(typeof(List<GetTaskListResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTaskListAsync(
        [FromQuery] GetTaskListQuery query,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
           result => Ok(result),
           errors => Problem(errors));
    }

    [HttpGet("Project/{projectId}")]
    [ProducesResponseType(typeof(List<TaskResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProjectTasksAsync(
        int projectId,
        [FromQuery] GetProjectTasksRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetProjectTasksQuery(
            projectId, 
            request.Filter,
            request.SortBy);

        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
           result => Ok(result),
           errors => Problem(errors));
    }
}

public class GetProjectTasksRequest
{
    public TaskFilter Filter { get; set; } = new();

    public string? SortBy { get; set; }
}