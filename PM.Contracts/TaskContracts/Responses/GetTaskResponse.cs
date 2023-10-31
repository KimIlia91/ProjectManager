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

public class ProjectResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}

public class EmployeeResponse
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string MiddelName { get; set; } = string.Empty;

    public string Email { get; set; } = null!;
}