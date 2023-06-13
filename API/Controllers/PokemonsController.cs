using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonsController : ControllerBase
    {
        private readonly StoreContext context;
        public PokemonsController(StoreContext context)
        {
            this.context = context;
        }
    
        // Return Products in JSON format
        [HttpGet]
        public async Task<ActionResult<List<Pokemon>>> GetPokemons()
        {
            return await this.context.Pokemons.ToListAsync();
        }

        // Return a product in JSON format, given a route of the product requested 
        [HttpGet("{name}")]
        public async Task<ActionResult<Pokemon>> GetPokemon(string name)
        {
            return await this.context.Pokemons.FirstOrDefaultAsync(pokemon => pokemon.Name == name);
        }
    }
}