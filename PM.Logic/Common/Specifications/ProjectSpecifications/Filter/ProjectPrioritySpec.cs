using PM.Application.Common.Specifications.ISpecifications;
using PM.Domain.Common.Enums;
using PM.Domain.Entities;
using System.Linq.Expressions;

namespace PM.Application.Common.Specifications.ProjectSpecifications.Filter;

/// <summary>
/// Represents a specification for filtering projects by priority.
/// </summary>
internal class ProjectPrioritySpec : ISpecification<Project>
{
    private readonly Priority? _priority;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectPrioritySpec"/> class.
    /// </summary>
    /// <param name="priority">The priority to filter by.</param>
    public ProjectPrioritySpec(Priority? priority)
    {
        _priority = priority;
    }

    /// <summary>
    /// Converts the specification to an expression.
    /// </summary>
    /// <returns>An expression representing the filtering condition for projects based on priority.</returns>
    public Expression<Func<Project, bool>> ToExpression()
    {
        return p => !_priority.HasValue || p.Priority == _priority;
    }
}
