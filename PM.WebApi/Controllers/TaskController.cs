using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.Application.Common.Models.Task;
using PM.Application.Features.TaskContext.Commands.ChangeTaskStatus;
using PM.Application.Features.TaskContext.Commands.CreateTask;
using PM.Application.Features.TaskContext.Commands.DeleteTask;
using PM.Application.Features.TaskContext.Commands.UpdateTask;
using PM.Application.Features.TaskContext.Dtos;
using PM.Application.Features.TaskContext.Queries.GetTask;
using PM.Application.Features.TaskContext.Queries.GetTaskList;
using PM.Application.Features.TaskContext.Queries.GetTaskListOfCurrentUser;
using PM.Application.Features.TaskContext.Queries.GetTaskListOfProject;
using PM.Application.Features.TaskContext.Queries.GetTaskListOfProjectByUser;
using PM.Application.Features.TaskContext.Queries.GetTaskOfCurrentUser;
using PM.Domain.Common.Constants;

namespace PM.WebApi.Controllers;

/// <summary>
/// Controller for managing tasks.
/// </summary>
public class TaskController : ApiBaseController
{
    /// <summary>
    /// Create a new task.
    /// </summary>
    /// <param name="command">The command for creating the task.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult representing the operation result, which can be one of the following:
    /// - 200 OK with the created task if successful.
    /// - A problem response with errors if the operation encounters issues.
    /// </returns>
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

    /// <summary>
    /// Update an existing task.
    /// </summary>
    /// <param name="command">The command for updating the task.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult representing the operation result, which can be one of the following:
    /// - 200 OK with the updated task if successful.
    /// - A problem response with errors if the operation encounters issues.
    /// </returns>
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

    /// <summary>
    /// Retrieve a task by its ID.
    /// </summary>
    /// <param name="id">The ID of the task to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult representing the retrieved task if found.
    /// - 200 OK with the task details if successful.
    /// - A problem response with errors if the task is not found or if there are issues.
    /// </returns>
    [HttpGet("{id}")]
    [Authorize(Roles = RoleConstants.Supervisor)]
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
    /// Delete a task by its ID.
    /// </summary>
    /// <param name="id">The ID of the task to delete.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult indicating the result of the deletion operation, which is:
    /// - 204 No Content: If the task is successfully deleted.
    /// - A problem response with errors if the task is not found or if there are issues.
    /// </returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteTaskAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteTaskCommand { TaskId = id };
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match(
            result => NoContent(),
            errors => Problem(errors));
    }

    /// <summary>
    /// Get task list.
    /// </summary>
    /// <param name="query">The query parameters for filtering and sorting tasks.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult representing the list of tasks matching the query.
    /// - 200 OK with the list of tasks if successful.
    /// - A problem response with errors if there are issues.
    /// </returns>
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
    /// Get task list of project.
    /// </summary>
    /// <param name="id">Project ID.</param>
    /// <param name="request">Query parameters for filtering and sorting tasks.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>Task list or error.</returns>
    [HttpGet("Project/{id}")]
    [ProducesResponseType(typeof(List<TaskResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTaskListOfProjectAsync(
        int id,
        [FromQuery] GetProjectTasksRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetTaskListOfProjectQuery(
            id,
            request.Filter,
            request.SortBy);

        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
           result => Ok(result),
           errors => Problem(errors));
    }

    /// <summary>
    /// Retrieve a list of tasks associated with a specific project.
    /// </summary>
    /// <param name="id">The ID of the project to retrieve tasks for.</param>
    /// <param name="request">Query parameters for filtering and sorting tasks.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult representing the list of tasks associated with the project.
    /// - 200 OK with the list of tasks if successful.
    /// - A problem response with errors if there are issues.
    /// </returns>
    [HttpGet("Project/{id}/User")]
    [ProducesResponseType(typeof(List<TaskResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTaskListOfProjectByUserAsync(
        int id,
        [FromQuery] GetProjectTasksRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetTaskListOfProjectByUserQuery(
            id,
            request.Filter,
            request.SortBy);

        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
           result => Ok(result),
           errors => Problem(errors));
    }

    /// <summary>
    /// Get task list of current user.
    /// </summary>
    /// <param name="query">Query parameters for filtering and sorting tasks.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>Task list of current user.</returns>
    [HttpGet("User")]
    [ProducesResponseType(typeof(ChangeTaskStatusResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTaskListOfCurrentUserAsync(
        [FromQuery] GetTaskListOfCurrentUserQuery query,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
           result => Ok(result),
           errors => Problem(errors));
    }

    /// <summary>
    /// Get task of current user.
    /// </summary>
    /// <param name="id">ID of task.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>Task of current user.</returns>
    [HttpGet("{id}/user")]
    [ProducesResponseType(typeof(ChangeTaskStatusResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTaskOfCurrentUserAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var query = new GetTaskOfCurrentUserQuery(id);
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
           result => Ok(result),
           errors => Problem(errors));
    }

    /// <summary>
    /// Changes the status of a task.
    /// </summary>
    /// <param name="command">The command to change the task status.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An IActionResult representing the result of the operation.</returns>
    [HttpPut("Status")]
    [ProducesResponseType(typeof(ChangeTaskStatusResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangeTaskStatusAsync(
        ChangeTaskStatusCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

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