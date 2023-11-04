namespace PM.Application.Features.TaskContext.Dtos;

/// <summary>
/// Represents the result of a task creation operation.
/// </summary>
/// <param name="Id">The unique identifier of the created task.</param>
public sealed record CreateTaskResult(int Id);