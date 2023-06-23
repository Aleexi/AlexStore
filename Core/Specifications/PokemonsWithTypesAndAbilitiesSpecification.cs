using Core.Entities;

namespace Core.Specifications
{
    public class PokemonsWithTypesAndAbilitiesSpecification : Specification<Pokemon>
    {
        public PokemonsWithTypesAndAbilitiesSpecification(PokemonSpecificationParams pokemonParams) : base(x => 
            (string.IsNullOrEmpty(pokemonParams.Search) || x.Name.ToLower().Contains(pokemonParams.Search)) &&
            (!pokemonParams.TypeId.HasValue || x.PokemonTypeId == pokemonParams.TypeId) && 
            (!pokemonParams.AbilitieId.HasValue || x.PokemonAbilitieId == pokemonParams.AbilitieId))
        {
            AddInclude(p => p.PokemonType);
            AddInclude(p => p.PokemonAbilitie);
            ApplyPaging(pokemonParams.PageSize * (pokemonParams.PageIndex - 1), pokemonParams.PageSize);

            if (!string.IsNullOrEmpty(pokemonParams.Sort))
            {
                if (pokemonParams.Sort == "strengthAsc")
                {
                    AddOrderBy(p => p.Strength);
                }
                else if (pokemonParams.Sort == "strengthDesc")
                {
                    AddOrderByDescending(p => p.Strength);
                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            }    
        }

        public PokemonsWithTypesAndAbilitiesSpecification(string name, string emptyParameter) : base(p => p.Name == name)
        {
            AddInclude(p => p.PokemonType);
            AddInclude(p => p.PokemonAbilitie);
        }
    }
}