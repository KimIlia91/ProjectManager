using ErrorOr;
using MediatR;
using PM.Application.Features.TaskContext.Dtos;

namespace PM.Application.Features.TaskContext.Queries.GetTask;

public sealed record GetTaskQuery(int Id) : IRequest<ErrorOr<GetTaskResult>>;
