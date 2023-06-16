using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaulation<T> where T : SuperEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, InterfaceSpecification<T> specification)
        {
            // p => p.Id = id
            if (specification.Condition != null){
                inputQuery = inputQuery.Where(specification.Condition);
            }

            // Creating the .Include(p => p.ProductType).Include(p => p.ProductBrand)
            inputQuery = specification.IncludesList.Aggregate(inputQuery, (current, include) => current.Include(include));

            return inputQuery;
        }
    }
}