using PM.Domain.Common.Enums;

namespace PM.Application.Features.ProjectContext.Dtos;

public class GetManagerProjectListResult
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string CustomerCompany { get; set; } = null!;

    public string ExecutorCompany { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public Priority Priority { get; set; }
}