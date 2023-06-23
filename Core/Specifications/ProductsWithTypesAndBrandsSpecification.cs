using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : Specification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecificationParams productParams) : base(x => 
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId) && 
            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);
            
            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                if (productParams.Sort == "priceAsc")
                {
                    AddOrderBy(p => p.Price);
                }
                else if (productParams.Sort == "priceDesc")
                {
                    AddOrderByDescending(p => p.Price);
                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}