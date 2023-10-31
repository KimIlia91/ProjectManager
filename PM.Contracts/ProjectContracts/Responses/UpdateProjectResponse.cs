namespace PM.Contracts.ProjectContracts.Responses;

public sealed class UpdateProjectResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CustomerCompanyId { get; set; }

    public int ExecutorCompanyId { get; set; }

    public int ManagerId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int Priority { get; set; }
}
