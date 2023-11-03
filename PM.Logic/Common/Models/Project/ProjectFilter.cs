using PM.Domain.Common.Enums;

namespace PM.Application.Common.Models.Project;

/// <summary>
/// Represents a set of filter criteria for querying projects.
/// </summary>
public class ProjectFilter
{
    /// <summary>
    /// Gets or sets the priority level to filter projects. Can be null.
    /// </summary>
    public Priority? Priority { get; set; }

    /// <summary>
    /// Gets or sets the start date to filter projects. Can be null.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the start date to filter projects. Can be null.
    /// </summary>
    public DateTime? EndDate { get; set; }
}
