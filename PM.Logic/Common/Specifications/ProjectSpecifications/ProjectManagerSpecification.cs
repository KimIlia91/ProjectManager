using PM.Application.Common.Specifications.ISpecifications;
using PM.Domain.Entities;
using System.Linq.Expressions;

namespace PM.Application.Common.Specifications.ProjectSpecifications;

internal sealed class ProjectManagerSpecification : ISpecification<Project>
{
    private readonly int? _managerId;

    public ProjectManagerSpecification(
        int? managerId)
    {
        _managerId = managerId;
    }

    public Expression<Func<Project, bool>> ToExpression()
    {
        return p => (!_managerId.HasValue 
            || (_managerId.HasValue && _managerId == 0 && p.Manager == null) 
            || p.Manager.Id == _managerId);
    }
}
