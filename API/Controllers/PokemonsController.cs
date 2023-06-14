using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonsController : ControllerBase
    {
        private readonly InterfaceRepository repository;
        
        public PokemonsController(InterfaceRepository repository)
        {
            this.repository = repository;
        }
    
        // Return Products in JSON format
        [HttpGet]
        public async Task<ActionResult<List<Pokemon>>> GetPokemons()
        {
            return Ok(await this.repository.GetPokemonsAsync());
        }

        // Return a product in JSON format, given a route of the product requested 
        [HttpGet("{name}")]
        public async Task<ActionResult<Pokemon>> GetPokemon(string name)
        {
            return Ok(await this.repository.GetPokemonNameAsync(name));
        }

        [HttpGet("abilities")]
        public async Task<ActionResult<IReadOnlyList<PokemonAbilitie>>> GetPokemonAbilities()
        {
            return Ok(await this.repository.GetPokemonAbilitiesAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<PokemonType>>> GetPokemonTypes()
        {
            return Ok(await this.repository.GetPokemonTypesAsync());
        }

    }
}