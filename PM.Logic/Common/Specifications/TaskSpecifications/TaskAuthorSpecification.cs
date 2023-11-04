using PM.Application.Common.Specifications.ISpecifications;
using System.Linq.Expressions;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Common.Specifications.TaskSpecifications;

/// <summary>
/// Represents a specification for filtering tasks by author.
/// </summary>
internal class TaskAuthorSpecification : ISpecification<Task>
{
    private readonly int? _authorId;

    /// <summary>
    /// Initializes a new instance of the <see cref="TaskAuthorSpecification"/> class.
    /// </summary>
    /// <param name="employeeId">The ID of the author to filter by.</param>
    public TaskAuthorSpecification(int? employeeId)
    {
        _authorId = employeeId;
    }

    /// <summary>
    /// Converts the specification to an expression.
    /// </summary>
    /// <returns>An expression representing the filtering condition for tasks based on the author's ID.</returns>
    public Expression<Func<Task, bool>> ToExpression()
    {
        return p => (!_authorId.HasValue
            || (_authorId.HasValue && _authorId == 0 && p.Author == null)
            || p.Author.Id == _authorId);
    }
}