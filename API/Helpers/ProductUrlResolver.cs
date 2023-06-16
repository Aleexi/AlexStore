using API.DTO;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration config;
        public ProductUrlResolver(IConfiguration config)
        {
            this.config = config;
        }

        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
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