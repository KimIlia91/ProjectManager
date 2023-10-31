using PM.Contracts.EmployeeContracts.Responses;
using PM.Contracts.TaskContracts.Responses;

namespace PM.Contracts.ProjectContracts.Responses;

public sealed class GetProjectResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string CustomerCompany { get; set; } = null!;

    public string ExecutorCompany { get; set; } = null!;

    public GetEmployeeResponse Manager { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int Priority { get; set; }

    public List<TaskResponse> Tasks { get; set; } = new List<TaskResponse>();
}

public class TaskResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Status { get; set; }

    public EmployeeResponse Executor { get; set; }
}