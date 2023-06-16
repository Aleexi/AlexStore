using API.DTO;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(p => p.ProductType, x => x.MapFrom(d => d.ProductType.Name))
                .ForMember(p => p.ProductBrand, x => x.MapFrom(d => d.ProductBrand.Name))
                .ForMember(p => p.PictureURL, x => x.MapFrom<ProductUrlResolver>());

            CreateMap<Pokemon, PokemonDTO>()
                .ForMember(p => p.PokemonType, x => x.MapFrom(d => d.PokemonType.Name))
                .ForMember(p => p.PokemonAbilitie, x => x.MapFrom(d => d.PokemonAbilitie.Name))
                .ForMember(p => p.PictureURL, x => x.MapFrom<PokemonUrlResolver>());
        }
    }
}