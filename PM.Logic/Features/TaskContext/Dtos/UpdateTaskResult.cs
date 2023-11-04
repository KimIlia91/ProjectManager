using Mapster;
using PM.Domain.Common.Enums;
using Task = PM.Domain.Entities.Task;
using Status = PM.Domain.Common.Enums.Status;

namespace PM.Application.Features.TaskContext.Dtos;

/// <summary>
/// Represents the result of updating a task.
/// </summary>
public sealed class UpdateTaskResult : IRegister
{
    /// <summary>
    /// Gets or sets the unique identifier of the task.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the task.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the unique identifier of the author of the task.
    /// </summary>
    public int AuthorId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the executor of the task.
    /// </summary>
    public int ExecutorId { get; set; }

    /// <summary>
    /// Gets or sets the comment associated with the task.
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Gets or sets the status of the task.
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// Gets or sets the priority of the task.
    /// </summary>
    public Priority Priority { get; set; }

    /// <summary>
    /// Registers type mappings for the UpdateTaskResult class.
    /// </summary>
    /// <param name="config">The TypeAdapterConfig to which mappings are registered.</param>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Task, UpdateTaskResult>()
            .Map(dest => dest.AuthorId, src => src.Author.Id)
            .Map(dest => dest.ExecutorId, src => src.Executor.Id);
    }
}
