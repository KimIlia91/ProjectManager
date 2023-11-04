using ErrorOr;
using MediatR;
using PM.Application.Features.TaskContext.Dtos;

namespace PM.Application.Features.TaskContext.Queries.GetTask;

/// <summary>
/// Represents a query to retrieve a task by its unique identifier.
/// </summary>
public sealed record GetTaskQuery(int Id) : IRequest<ErrorOr<GetTaskResult>>;
