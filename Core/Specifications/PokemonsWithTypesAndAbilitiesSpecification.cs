using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class PokemonsWithTypesAndAbilitiesSpecification : Specification<Pokemon>
    {
        public PokemonsWithTypesAndAbilitiesSpecification()
        {
            AddInclude(p => p.PokemonType);
            AddInclude(p => p.PokemonAbilitie);
        }

        public PokemonsWithTypesAndAbilitiesSpecification(string name) : base(p => p.Name == name)
        {
            AddInclude(p => p.PokemonType);
            AddInclude(p => p.PokemonAbilitie);
        }
    }
}