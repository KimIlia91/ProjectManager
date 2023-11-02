using PM.Application.Common.Models.Employee;
using PM.Application.Common.Models.Task;
using PM.Domain.Common.Enums;

namespace PM.Application.Features.ProjectContext.Dtos;

public class GetProjectResult
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string CustomerCompany { get; set; } = null!;

    public string ExecutorCompany { get; set; } = null!;

    public EmployeeResult? Manager { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public Priority Priority { get; set; }

    public List<TaskResult> Tasks { get; set; } = new List<TaskResult>();
}
