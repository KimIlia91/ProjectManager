using PM.Application.Common.Specifications.ISpecifications;
using PM.Application.Common.Specifications.ProjectSpecifications.Filter;
using PM.Domain.Entities;
using System.Linq.Expressions;

namespace PM.Application.Common.Specifications.ProjectSpecifications;

internal class ProjectsOfUserSpec : ISpecification<Project>
{
    private readonly int _userId;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectPrioritySpec"/> class.
    /// </summary>
    /// <param name="priority">The priority to filter by.</param>
    public ProjectsOfUserSpec(int userId)
    {
        _userId = userId;
    }

    /// <summary>
    /// Converts the specification to an expression.
    /// </summary>
    /// <returns>An expression representing the filtering condition for projects based on priority.</returns>
    public Expression<Func<Project, bool>> ToExpression()
    {
        return p => p.Users.Any(e => e.Id == _userId) || 
            (p.Manager != null && p.Manager.Id == _userId);
    }
}
