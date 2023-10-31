using ErrorOr;
using MediatR;
using PM.Application.Features.TaskContext.Dtos;
using PM.Domain.Common.Enums;
using PM.Domain.Entities;
using System.Text.Json.Serialization;
using Task = PM.Domain.Entities.Task;
using TaskStatus = PM.Domain.Common.Enums.TaskStatus;

namespace PM.Application.Features.TaskContext.Commands.UpdateTask;

public sealed class UpdateTaskCommand : IRequest<ErrorOr<UpdateTaskResult>>
{
    [JsonIgnore] public Task? Task { get; set; }

    [JsonIgnore] public Employee? Author { get; set; }

    [JsonIgnore] public Employee? Executor { get; set; }

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int AuthorId { get; set; }

    public int ExecutorId { get; set; }

    public string? Comment { get; set; }

    public TaskStatus Status { get; set; }

    public ProjectPriority Priority { get; set; }
}
