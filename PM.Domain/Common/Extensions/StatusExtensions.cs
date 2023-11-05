using ErrorOr;
using PM.Domain.Common.Enums;

namespace PM.Domain.Common.Extensions;

public static class StatusExtensions
{
    public static ErrorOr<Status> ToStatusEnum(
        this string statusName)
    {
        return statusName.ToLower() switch
        {
            "todo" => Status.ToDo,
            "inprogress" => Status.InProgress,
            "done" => Status.Done,
            _ => Error.Validation("Status not found"),
        };
    }
}
