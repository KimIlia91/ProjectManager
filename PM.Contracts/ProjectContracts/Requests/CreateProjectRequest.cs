namespace PM.Contracts.ProjectContracts.Requests;

public sealed class CreateProjectRequest
{
    public string Name { get; set; } = null!;

    public int CustomerCompanyId { get; set; }

    public int ExecutorCompanyId { get; set; }

    public int ManagerId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int Priority { get; set; }
}
