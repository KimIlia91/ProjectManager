using PM.Contracts.EmployeeContracts.Responses;

namespace PM.Contracts.TaskContracts.Responses;

public class TaskResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Status { get; set; }

    public EmployeeResponse Executor { get; set; } = new EmployeeResponse();
}