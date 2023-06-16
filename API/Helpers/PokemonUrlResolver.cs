using API.DTO;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class PokemonUrlResolver : IValueResolver<Pokemon, PokemonDTO, string>
    {
        private readonly IConfiguration config;
        public PokemonUrlResolver(IConfiguration config)
        {
            this.config = config;
        }

        public string Resolve(Pokemon source, PokemonDTO destination, string destMember, ResolutionContext context)
        {
            // If there is a PictureUrl, return it. 
            if (!string.IsNullOrEmpty(source.PictureURL))
            {
                return this.config["ApiUrl"] + source.PictureURL;
            }

            return null;
        }
    }
}