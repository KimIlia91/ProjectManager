using PM.Application.Common.Models.Employee;
using PM.Domain.Common.Enums;

namespace PM.Application.Features.ProjectContext.Dtos;

/// <summary>
/// Represents the result of retrieving a list of projects.
/// </summary>
public sealed class GetProjectListResult
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string CustomerCompany { get; set; } = null!;

    public string ExecutorCompany { get; set; } = null!;

    public UserResult Manager { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public Priority Priority { get; set; }
}