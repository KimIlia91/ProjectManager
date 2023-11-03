using ErrorOr;
using MediatR;
using PM.Application.Features.TaskContext.Dtos;
using PM.Domain.Common.Enums;
using PM.Domain.Entities;
using System.Text.Json.Serialization;
using Status = PM.Domain.Common.Enums.Status;

namespace PM.Application.Features.TaskContext.Commands.CreateTask;

public sealed class CreateTaskCommand 
    : IRequest<ErrorOr<CreateTaskResult>>
{
    [JsonIgnore] public User? Author { get; set; }

    [JsonIgnore] public User? Executor { get; set; }

    [JsonIgnore] public Project? Project { get; set; }

    public int ProjectId { get; set; }

    public string Name { get; set; } = null!;

    public int AuthorId { get; set; }

    public int ExecutorId { get; set; }

    public string? Comment { get; set; }

    public Status Status { get; set; }

    public Priority Priority { get; set; }
}