using ErrorOr;
using MediatR;
using PM.Application.Common.Models.Task;

namespace PM.Application.Features.TaskContext.Queries.GetTaskOfCurrentUser;

public sealed record GetTaskOfCurrentUserQuery(
    int TaskId) : IRequest<ErrorOr<TaskResult>>;
