using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface InterfaceSpecification<T>
    {
        // Expression takes a function, type and return type
        Expression<Func<T, bool>> Condition {get; } // "Where" critera
        List<Expression<Func<T, object>>> IncludesList {get; } // Includes, list because we might want to chain several Includes

        Expression<Func<T, object>> OrderBy {get; } // OrderBy functionality 
        Expression<Func<T, object>> OrderByDescending {get; } // OrderByDesc functionality 
        public int Take { get; }
        public int Skip { get; }
        public bool isPagingEnabled { get; }

    }
}