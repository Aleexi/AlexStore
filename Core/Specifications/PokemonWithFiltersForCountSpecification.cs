using Core.Entities;

namespace Core.Specifications
{
    public class PokemonWithFiltersForCountSpecification : Specification<Pokemon>
    {
        public PokemonWithFiltersForCountSpecification(PokemonSpecificationParams pokemonParams) : base(x => 
            (string.IsNullOrEmpty(pokemonParams.Search) || x.Name.ToLower().Contains(pokemonParams.Search)) &&
            (!pokemonParams.TypeId.HasValue || x.PokemonTypeId == pokemonParams.TypeId) && 
            (!pokemonParams.AbilitieId.HasValue || x.PokemonAbilitieId == pokemonParams.AbilitieId))
        {
        }
    }
}