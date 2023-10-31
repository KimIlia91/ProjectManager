using Mapster;
using PM.Domain.Common.Enums;
using Task = PM.Domain.Entities.Task;
using TaskStatus = PM.Domain.Common.Enums.TaskStatus;

namespace PM.Application.Features.TaskContext.Dtos;

public sealed class UpdateTaskResult : IRegister
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int AuthorId { get; set; }

    public int ExecutorId { get; set; }

    public string? Comment { get; set; }

    public TaskStatus Status { get; set; }

    public ProjectPriority Priority { get; set; }

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Task, UpdateTaskResult>()
            .Map(dest => dest.AuthorId, src => src.Author.Id)
            .Map(dest => dest.ExecutorId, src => src.Executor.Id);
    }
}