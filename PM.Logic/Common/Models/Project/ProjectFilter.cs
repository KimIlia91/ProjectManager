using PM.Domain.Common.Enums;

namespace PM.Application.Common.Models.Project;

public class ProjectFilter
{
    public Priority? Priority { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
