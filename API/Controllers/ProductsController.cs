using API.DTO;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
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
        public async Task<ActionResult<IReadOnlyList<ProductDTO>>> GetProducts()
        {
            var specification = new ProductsWithTypesAndBrandsSpecification();

            var products = await this.productsRepository.GetListWithSpecification(specification);

            return Ok(this.mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDTO>>(products));
        }

        // Return a product in JSON format, given a route of the product requested 
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await this.productsRepository.GetEntityWithSpecification(specification);

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