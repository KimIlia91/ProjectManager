using System.Linq.Expressions;

namespace PM.Application.Common.Specifications.ISpecifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> ToExpression();
}
