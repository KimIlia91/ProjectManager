using PM.Application.Features.EmployeeContext.Dtos;
using PM.Domain.Common.Enums;
using TaskStatus = PM.Domain.Common.Enums.TaskStatus;

namespace PM.Application.Features.ProjectContext.Dtos;

public class GetProjectResult
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string CustomerCompany { get; set; } = null!;

    public string ExecutorCompany { get; set; } = null!;

    public GetEmployeeResult Manager { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public ProjectPriority Priority { get; set; }

    public List<TaskResult> Tasks { get; set; } = new List<TaskResult>();
}

public class TaskResult
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public EmployeeResult Executor { get; set; } = null!;

    public TaskStatus Status { get; set; }

    public ProjectPriority Priority { get; set; }
}

public class EmployeeResult
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string MiddelName { get; set; } = string.Empty;

    public string Email { get; set; } = null!;
}