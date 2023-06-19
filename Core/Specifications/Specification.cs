using System.Linq.Expressions;

namespace Core.Specifications
{
    public class Specification<T> : InterfaceSpecification<T>
    {
        public Specification()
        {
        }

        public Specification(Expression<Func<T, bool>> condition)
        {
            Condition = condition;
        }

        public Expression<Func<T, bool>> Condition {get; }

        public List<Expression<Func<T, object>>> IncludesList {get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy {get; private set;}

        public Expression<Func<T, object>> OrderByDescending {get; private set;}

        public int Take {get; private set;}

        public int Skip {get; private set;}

        public bool isPagingEnabled {get; private set;}

        protected void AddInclude(Expression<Func<T, object>> includes)
        {
            IncludesList.Add(includes);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        protected void ApplyPaging(int skip, int take)
        {
            this.Skip = skip;
            this.Take = take;
            this.isPagingEnabled = true;
        }
    }
}