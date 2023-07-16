using API.DTO;
using AutoMapper;
using Core.Entities;
using Core.OrderAggregation;

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
            
            CreateMap<Core.Entities.Identity.Address, AddressDTO>().ReverseMap();
            CreateMap<AddressDTO, Core.OrderAggregation.Address>();
            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(destination => destination.DelieveryMethod, options => options.MapFrom(source => 
                source.DelieveryMethod.ShortName))
                .ForMember(destination => destination.ShippingPrice, options => options.MapFrom(source => 
                source.DelieveryMethod.PriceOfDelievery));
    

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(destination => destination.Id, options => options.MapFrom(source => source.ItemOrdered.ItemId))
                .ForMember(destination => destination.Name, options => options.MapFrom(source => source.ItemOrdered.ItemName))
                .ForMember(destination => destination.PictureUrl, options => options.MapFrom(source => source.ItemOrdered.PictureUrl))
                .ForMember(destination => destination.PictureUrl, options => options.MapFrom<OrderItemUrlResolver>());

            CreateMap<CustomerBasketDTO, CustomerBasket>();
            CreateMap<BasketItemDTO, BasketItem>();
        }
    }
}