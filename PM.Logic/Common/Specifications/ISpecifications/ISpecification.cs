using System.Linq.Expressions;

namespace PM.Application.Common.Specifications.ISpecifications;

/// <summary>
/// Represents a generic specification interface for building expressions.
/// </summary>
/// <typeparam name="T">The type for which the specification is defined.</typeparam>
public interface ISpecification<T>
{
    /// <summary>
    /// Converts the specification to an expression.
    /// </summary>
    /// <returns>An expression that can be used to filter entities of type T.</returns>
    Expression<Func<T, bool>> ToExpression();
}
