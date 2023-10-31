using ErrorOr;
using MediatR;
using PM.Application.Features.TaskContext.Dtos;

namespace PM.Application.Features.TaskContext.Commands.DeleteTask;

public sealed record DeleteTaskCommand(int Id) : IRequest<ErrorOr<DeleteTaskResult>>;
