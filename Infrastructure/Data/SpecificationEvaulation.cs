using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaulation<T> where T : SuperEntity
    {
        // Andra parametern var InterfaceSpecification från början! om så behövs ändra tillbaka
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, Specification<T> specification)
        {
            var builtQuery = inputQuery;

            // p => p.Id = id
            if (specification.Condition != null){
                builtQuery = builtQuery.Where(specification.Condition);
            }

            if (specification.OrderBy != null){
                builtQuery = builtQuery.OrderBy(specification.OrderBy);
            }

            else if (specification.OrderByDescending != null){
                builtQuery = builtQuery.OrderByDescending(specification.OrderByDescending);

            }

            if (specification.isPagingEnabled)
            {
                builtQuery = builtQuery.Skip(specification.Skip).Take(specification.Take);
            }

            // Creating the .Include(p => p.ProductType).Include(p => p.ProductBrand)
            builtQuery = specification.IncludesList.Aggregate(builtQuery, (current, include) => current.Include(include));

            return builtQuery;
        }
    }
}