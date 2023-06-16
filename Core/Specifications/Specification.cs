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

        protected void AddInclude(Expression<Func<T, object>> includes){
            IncludesList.Add(includes);
        }
    }
}