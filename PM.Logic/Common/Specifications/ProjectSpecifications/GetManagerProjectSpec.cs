using PM.Application.Common.Specifications.ISpecifications;
using PM.Domain.Entities;
using System.Linq.Expressions;

namespace PM.Application.Common.Specifications.ProjectSpecifications;

internal class GetManagerProjectSpec : ISpecification<Project>
{
    private readonly int _projectId;
    private readonly int _userId;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectPrioritySpecification"/> class.
    /// </summary>
    /// <param name="priority">The priority to filter by.</param>
    public GetManagerProjectSpec(
        int projectId,
        int userId)
    {
        _projectId = projectId;
        _userId = userId;
    }

    /// <summary>
    /// Converts the specification to an expression.
    /// </summary>
    /// <returns>An expression representing the filtering condition for projects based on priority.</returns>
    public Expression<Func<Project, bool>> ToExpression()
    {
        return p => p.Id == _projectId && p.Manager.Id == _userId;
    }
}