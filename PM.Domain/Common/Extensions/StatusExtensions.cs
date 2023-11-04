using ErrorOr;
using PM.Domain.Common.Enums;

namespace PM.Domain.Common.Extensions;

public static class StatusExtensions
{
    public static Status GetStatus(
        this string statusName)
    {
        return statusName switch
        {
            "ToDo" => Status.ToDo,
            "InProgress" => Status.InProgress,
            "Done" => Status.Done,
            _ => Status.ToDo,
        };
    }
}
