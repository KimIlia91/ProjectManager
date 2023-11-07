using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Specifications.ISpecifications;
using PM.Domain.Entities;
using System.Linq.Expressions;

namespace PM.Application.Common.Specifications.ProjectSpecifications;

/// <summary>
/// 
/// </summary>
internal class ProjectOfManagerSpec : ISpecification<Project>
{
    private readonly int _projectId;
    private readonly int _userId;
    private readonly ICurrentUserService _currentUserService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="currentUserService"></param>
    public ProjectOfManagerSpec(
        int projectId,
        ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
        _projectId = projectId;
        _userId = _currentUserService.UserId;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Expression<Func<Project, bool>> ToExpression()
    {
        if (_currentUserService.IsSupervisor)
            return p => p.Id == _projectId;

        return p => p.Id == _projectId && p.Manager != null && p.ManagerId == _userId;
    }
}