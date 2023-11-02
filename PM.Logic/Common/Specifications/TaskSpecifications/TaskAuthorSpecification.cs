using PM.Application.Common.Specifications.ISpecifications;
using System.Linq.Expressions;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Common.Specifications.TaskSpecifications;

internal class TaskAuthorSpecification : ISpecification<Task>
{
    private readonly int? _authorId;

    public TaskAuthorSpecification(
        int? employeeId)
    {
        _authorId = employeeId;
    }

    public Expression<Func<Task, bool>> ToExpression()
    {
        return p => (!_authorId.HasValue
            || (_authorId.HasValue && _authorId == 0 && p.Author == null)
            || p.Author.Id == _authorId);
    }
}
