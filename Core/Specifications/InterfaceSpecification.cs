using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface InterfaceSpecification<T>
    {
        // Expression takes a function, type and return type
        Expression<Func<T, bool>> Condition {get; } // "Where" critera
        List<Expression<Func<T, object>>> IncludesList {get; } // Includes, list because we might want to chain several Includes
    }
}