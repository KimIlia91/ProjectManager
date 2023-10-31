using PM.Application.Features.CompanyContext.Dtos;
using PM.Application.Features.EmployeeContext.Dtos;
using PM.Domain.Common.Enums;

namespace PM.Application.Features.ProjectContext.Dtos;

public class GetProjectResult 
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public GetCompanyResult CustomerCompany { get; set; } = null!;

    public GetCompanyResult ExecutorCompany { get; set; } = null!;

    public GetEmployeeResult Manager { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public ProjectPriority Priority { get; set; }
}
