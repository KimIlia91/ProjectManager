using PM.Domain.Common.Enums;
using TaskStatus = PM.Domain.Common.Enums.TaskStatus;

namespace PM.Application.Features.TaskContext.Dtos;

public class GetTaskResult
{
    public int Id { get; set; }

    public EmployeeResult Author { get; set; } = new EmployeeResult();

    public EmployeeResult Executor { get; set; } = new EmployeeResult();

    public ProjectResult Project { get; set; } = new ProjectResult();

    public string Commnet { get; set; } = string.Empty;

    public TaskStatus Status { get; set; }

    public ProjectPriority Priority { get; set; }
}

public class ProjectResult
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}

public class EmployeeResult
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string MiddelName { get; set; } = string.Empty;

    public string Email { get; set; } = null!;
}