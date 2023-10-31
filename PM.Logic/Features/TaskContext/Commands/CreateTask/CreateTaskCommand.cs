using ErrorOr;
using MediatR;
using PM.Application.Features.TaskContext.Dtos;
using PM.Domain.Common.Enums;
using PM.Domain.Entities;
using System.Text.Json.Serialization;
using AppTaskStatus = PM.Domain.Common.Enums.TaskStatus;

namespace PM.Application.Features.TaskContext.Commands.CreateTask;

public sealed class CreateTaskCommand 
    : IRequest<ErrorOr<CreateTaskResult>>
{
    [JsonIgnore] public Employee? Author { get; set; }

    [JsonIgnore] public Employee? Executor { get; set; }

    [JsonIgnore] public Project? Project { get; set; }

    public int ProjectId { get; set; }

    public string Name { get; set; } = null!;

    public int AuthorId { get; set; }

    public int ExecutorId { get; set; }

    public string? Commnet { get; set; }

    public AppTaskStatus Status { get; set; }

    public ProjectPriority Priority { get; set; }
}