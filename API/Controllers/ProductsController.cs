using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly InterfaceRepository repository;
    
        // Get access to the database through DbContext or StoreContext rather
        public ProductsController(InterfaceRepository repository)
        {         
            this.repository = repository;
            
        }

        // Return Products in JSON format or List
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            // Adding Asynchronous to not block threads handling HTTP requests.
            return Ok(await this.repository.GetProductsAsync());
        }

        // Return a product in JSON format, given a route of the product requested 
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return Ok(await this.repository.GetProductIdAsync(id));
        }

        // https://localhost5001/api/products/types
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await this.repository.GetProductTypesAsync());
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await this.repository.GetProductBrandsAsync());
        }
    }
}