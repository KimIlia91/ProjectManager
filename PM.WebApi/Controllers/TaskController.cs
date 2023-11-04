using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.Application.Common.Models.Task;
using PM.Application.Features.TaskContext.Commands.CreateTask;
using PM.Application.Features.TaskContext.Commands.DeleteTask;
using PM.Application.Features.TaskContext.Commands.UpdateTask;
using PM.Application.Features.TaskContext.Dtos;
using PM.Application.Features.TaskContext.Queries.GetProjectTasks;
using PM.Application.Features.TaskContext.Queries.GetTask;
using PM.Application.Features.TaskContext.Queries.GetTaskList;
using PM.Domain.Common.Constants;

namespace PM.WebApi.Controllers;

/// <summary>
/// 
/// </summary>
public class TaskController : ApiBaseController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = $"{RoleConstants.Supervisor}, {RoleConstants.Manager}")]
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize(Roles = $"{RoleConstants.Supervisor}, {RoleConstants.Manager}")]
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = $"{RoleConstants.Supervisor}, {RoleConstants.Manager}")]
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = $"{RoleConstants.Supervisor}")]
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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