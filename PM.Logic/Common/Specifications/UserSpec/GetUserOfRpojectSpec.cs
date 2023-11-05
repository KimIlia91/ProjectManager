using PM.Application.Common.Specifications.ISpecifications;
using PM.Domain.Entities;
using System.Linq.Expressions;

namespace PM.Application.Features.TaskContext.Commands.CreateTask.UserSpec;

public sealed class GetUserOfRpojectSpec : ISpecification<User>
{
    private readonly int _userId;
    private readonly int _projectId;

    public GetUserOfRpojectSpec(int userId, int projectId)
    {
        _userId = userId;
        _projectId = projectId;
    }

    /// <summary>
    /// Converts the specification to an expression.
    /// </summary>
    /// <returns>An expression representing the filtering condition for projects based on priority.</returns>
    public Expression<Func<User, bool>> ToExpression()
    {
        return u => u.Id == _userId &&
            u.Projects.Any(p => p.Id == _projectId);
    }
}
