using PM.Application.Common.Specifications.ISpecifications;
using PM.Domain.Entities;
using System.Linq.Expressions;

namespace PM.Application.Common.Specifications.ProjectSpecifications;

/// <summary>
/// Represents a specification for filtering projects by date range.
/// </summary>
internal sealed class ProjectDateSpecification : ISpecification<Project>
{
    private readonly DateTime? _startDate;
    private readonly DateTime? _endDate;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectDateSpecification"/> class.
    /// </summary>
    /// <param name="startDate">The start date of the date range to filter by.</param>
    /// <param name="endDate">The end date of the date range to filter by.</param>
    public ProjectDateSpecification(DateTime? startDate, DateTime? endDate)
    {
        _startDate = startDate;
        _endDate = endDate;
    }

    /// <summary>
    /// Converts the specification to an expression.
    /// </summary>
    /// <returns>An expression representing the date range filtering condition for projects.</returns>
    public Expression<Func<Project, bool>> ToExpression()
    {
        return p => (!_startDate.HasValue || p.StartDate.Date >= _startDate) &&
                    (!_endDate.HasValue || p.EndDate.Date <= _endDate);
    }
}
