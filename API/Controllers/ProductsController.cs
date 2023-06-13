using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        
        private readonly StoreContext context;

        // Get access to the database through DbContext or StoreContext rather
        public ProductsController(StoreContext context)
        {
            this.context = context;            
        }

        // Return Products in JSON format or List
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            // Adding Asynchronous to not block threads handling HTTP requests.
            return await this.context.Products.ToListAsync();
        }

        // Return a product in JSON format, given a route of the product requested 
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await this.context.Products.FindAsync(id);
        }
    }
}