using ErrorOr;
using MediatR;
using PM.Application.Common.Models.Task;

namespace PM.Application.Features.TaskContext.Queries.GetUserTaskList;

public sealed class GetUserTaskListQuery : IRequest<ErrorOr<TaskResult>>
{

}
