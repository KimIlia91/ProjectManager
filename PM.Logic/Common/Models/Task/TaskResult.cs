using PM.Application.Common.Models.Employee;
using PM.Domain.Common.Enums;
using Status = PM.Domain.Common.Enums.Status;

namespace PM.Application.Common.Models.Task;

public class TaskResult
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public UserResult Author { get; set; } = null!;

    public UserResult Executor { get; set; } = null!;

    public Status Status { get; set; }

    public Priority Priority { get; set; }
}
