using ErrorOr;
using MediatR;
using PM.Application.Common.Models.Task;

namespace PM.Application.Features.TaskContext.Queries.GetTaskListOfProject;

public sealed record GetTaskListOfProjectQuery(
    int ProjectId,
    TaskFilter Filter,
    string? SortBy) : IRequest<ErrorOr<List<TaskResult>>>;
