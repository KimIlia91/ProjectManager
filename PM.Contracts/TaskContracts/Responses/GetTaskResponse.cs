using PM.Contracts.ProjectContracts.Responses;

namespace PM.Contracts.TaskContracts.Responses;

public sealed class GetTaskResponse
{
    public int Id { get; set; }

    public EmployeeResponse Author { get; set; } = new EmployeeResponse();

    public EmployeeResponse Executor { get; set; } = new EmployeeResponse();

    public ProjectResponse Project { get; set; } = new ProjectResponse();

    public string Comment { get; set; } = string.Empty;

    public int Status { get; set; }

    public int Priority { get; set; }
}
