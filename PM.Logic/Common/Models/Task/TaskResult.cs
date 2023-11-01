using PM.Application.Common.Models.Employee;
using PM.Domain.Common.Enums;
using TaskStatus = PM.Domain.Common.Enums.TaskStatus;

namespace PM.Application.Common.Models.Task;

public class TaskResult
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public EmployeeResult Executor { get; set; } = null!;

    public TaskStatus Status { get; set; }

    public ProjectPriority Priority { get; set; }
}
