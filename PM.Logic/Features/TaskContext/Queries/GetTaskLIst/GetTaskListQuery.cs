using ErrorOr;
using MediatR;
using PM.Application.Features.TaskContext.Dtos;
using PM.Domain.Common.Enums;
using Status = PM.Domain.Common.Enums.Status;

namespace PM.Application.Features.TaskContext.Queries.GetTaskLIst;

public sealed class GetTaskListQuery : IRequest<ErrorOr<List<GetTaskResult>>>
{
    public Status Status { get; set; }

    public Priority Priority { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}
