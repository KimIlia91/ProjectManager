using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Specifications.ISpecifications;
using PM.Domain.Entities;
using System.Linq.Expressions;

namespace PM.Application.Common.Specifications.ProjectSpecifications;

internal class GetProjectOfManagerSpec : ISpecification<Project>
{
    private readonly int _projectId;
    private readonly int _userId;
    private readonly ICurrentUserService _currentUserService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectPrioritySpecification"/> class.
    /// </summary>
    /// <param name="priority">The priority to filter by.</param>
    public GetProjectOfManagerSpec(
        int projectId,
        ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
        _projectId = projectId;
        _userId = _currentUserService.UserId;
    }

    /// <summary>
    /// Converts the specification to an expression.
    /// </summary>
    /// <returns>An expression representing the filtering condition for projects based on priority.</returns>
    public Expression<Func<Project, bool>> ToExpression()
    {
        if (_currentUserService.IsSupervisor)
            return p => p.Id == _projectId;

        return p => p.Id == _projectId && p.Manager != null && p.Manager.Id == _userId;
    }
}