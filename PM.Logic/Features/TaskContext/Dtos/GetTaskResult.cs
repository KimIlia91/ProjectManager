using PM.Application.Common.Models.Employee;
using PM.Application.Common.Models.Project;
using PM.Domain.Common.Enums;
using TaskStatus = PM.Domain.Common.Enums.TaskStatus;

namespace PM.Application.Features.TaskContext.Dtos;

public class GetTaskResult
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public EmployeeResult Author { get; set; } = new EmployeeResult();

    public EmployeeResult Executor { get; set; } = new EmployeeResult();

    public ProjectResult Project { get; set; } = new ProjectResult();

    public string Comment { get; set; } = string.Empty;

    public TaskStatus Status { get; set; }

    public ProjectPriority Priority { get; set; }
}
