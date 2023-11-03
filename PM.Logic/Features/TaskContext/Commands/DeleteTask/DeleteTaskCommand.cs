using ErrorOr;
using MediatR;
using PM.Application.Features.TaskContext.Dtos;
using System.Text.Json.Serialization;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Features.TaskContext.Commands.DeleteTask;

/// <summary>
/// 
/// </summary>
public sealed class DeleteTaskCommand : IRequest<ErrorOr<DeleteTaskResult>>
{
    [JsonIgnore] public Task? Task { get; set; }

    public int Id { get; set; }
}