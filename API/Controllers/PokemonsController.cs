using API.DTO;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PokemonsController : SuperController
    {
        private readonly InterfaceRepository<PokemonAbilitie> pokemonsAbilitiesRepository;
        private readonly InterfaceRepository<PokemonType> pokemonsTypeRepository;
        private readonly InterfaceRepository<Pokemon> pokemonsRepository;
        private readonly IMapper mapper;
        
        public PokemonsController(InterfaceRepository<Pokemon> pokemonsRepository, 
        InterfaceRepository<PokemonType> pokemonsTypeRepository, 
        InterfaceRepository<PokemonAbilitie> pokemonsAbilitiesRepository,
        IMapper mapper)
        {
            this.pokemonsRepository = pokemonsRepository;
            this.pokemonsTypeRepository = pokemonsTypeRepository;
            this.pokemonsAbilitiesRepository = pokemonsAbilitiesRepository; 
            this.mapper = mapper;
        }
    
        // Return Products in JSON format
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<PokemonDTO>>> GetPokemons()
        {
            var specification = new PokemonsWithTypesAndAbilitiesSpecification();

            var pokemons = await this.pokemonsRepository.GetListWithSpecification(specification);

            return Ok(this.mapper.Map<IReadOnlyList<Pokemon>, IReadOnlyList<PokemonDTO>>(pokemons));
        }

        // Return a product in JSON format, given a route of the product requested 
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PokemonDTO>> GetPokemon(string name)
        {
            var specification = new PokemonsWithTypesAndAbilitiesSpecification(name);

            var pokemon = await this.pokemonsRepository.GetEntityWithSpecification(specification);

            if (pokemon == null){
                return NotFound(new ApiResponse(404));
            }

            return Ok(this.mapper.Map<Pokemon, PokemonDTO>(pokemon));
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<PokemonType>>> GetPokemonTypes()
        {
            return Ok(await this.pokemonsTypeRepository.GetListByGeneric());
        }

        [HttpGet("abilities")]
        public async Task<ActionResult<IReadOnlyList<PokemonAbilitie>>> GetPokemonAbilities()
        {
            return Ok(await this.pokemonsAbilitiesRepository.GetListByGeneric());
        }

        

    }
}