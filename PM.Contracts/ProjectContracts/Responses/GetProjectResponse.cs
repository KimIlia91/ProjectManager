using PM.Contracts.CompanyContracts.Reponses;
using PM.Contracts.EmployeeContracts.Responses;

namespace PM.Contracts.ProjectContracts.Responses;

public sealed class GetProjectResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public GetCompanyResponse CustomerCompany { get; set; } = null!;

    public GetCompanyResponse ExecutorCompany { get; set; } = null!;

    public GetEmployeeResponse Manager { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int Priority { get; set; }
}
