using API.DTO;
using API.Errors;
using AutoMapper;
using API.Helpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : SuperController
    {
        private readonly InterfaceRepository<Product> productsRepository;
        private readonly InterfaceRepository<ProductType> productsTypeRepository;
        private readonly InterfaceRepository<ProductBrand> productsBrandRepository;
        private readonly IMapper mapper;
    
        // Get access to the database through DbContext or StoreContext rather
        public ProductsController(InterfaceRepository<Product> productsRepository, 
        InterfaceRepository<ProductType> productsTypeRepository, 
        InterfaceRepository<ProductBrand> productsBrandRepository, 
        IMapper mapper)
        {   
            this.productsRepository = productsRepository;   
            this.productsTypeRepository = productsTypeRepository;
            this.productsBrandRepository = productsBrandRepository; 
            this.mapper = mapper;
        }

        // Return Products in JSON format or List
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDTO>>> GetProducts([FromQuery] ProductSpecificationParams productParams)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(productParams);

            var countSpecification = new ProductWithFiltersForCountSpecification(productParams);

            var totalItems = await this.productsRepository.CountAsync(countSpecification);

            var products = await this.productsRepository.GetListWithSpecification(specification);

            var data = this.mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDTO>>(products);

            return Ok(new Pagination<ProductDTO>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        }

        // Return a product in JSON format, given a route of the product requested 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await this.productsRepository.GetEntityWithSpecification(specification);

            if (product == null){
                return NotFound(new ApiResponse(404));
            }

            return Ok(this.mapper.Map<Product, ProductDTO>(product));
        }

        // https://localhost5001/api/products/types
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await this.productsTypeRepository.GetListByGeneric());
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await this.productsBrandRepository.GetListByGeneric());
        }
    }
}