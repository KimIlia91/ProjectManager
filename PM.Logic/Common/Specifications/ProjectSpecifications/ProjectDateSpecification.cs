using System.Linq.Expressions;
using PM.Domain.Entities;
using PM.Application.Common.Specifications.ISpecifications;

namespace PM.Application.Common.Specifications.ProjectSpecifications;

internal sealed class ProjectDateSpecification : ISpecification<Project>
{
    private readonly DateTime? _startDate;
    private readonly DateTime? _endDate;

    public ProjectDateSpecification(
        DateTime? startDate,
        DateTime? endDate)
    {
        _startDate = startDate;
        _endDate = endDate;
    }

    public Expression<Func<Project, bool>> ToExpression()
    {
        return p => (!_startDate.HasValue || p.StartDate.Date >= _startDate) &&
                    (!_endDate.HasValue || p.EndDate.Date <= _endDate);
    }
}
