using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class Repository : InterfaceRepository
    {
        private readonly StoreContext context;

        public Repository(StoreContext context)
        { this.context = context; }

        public async Task<IReadOnlyList<PokemonAbilitie>> GetPokemonAbilitiesAsync()
        { return await this.context.PokemonAbilities.ToListAsync(); }

        public async Task<Pokemon> GetPokemonNameAsync(string name)
        { return await this.context.Pokemons.Include(p => p.PokemonAbilitie).Include(p => p.PokemonType).FirstOrDefaultAsync(pokemon => pokemon.Name == name); }

        public async Task<IReadOnlyList<Pokemon>> GetPokemonsAsync()
        { return await this.context.Pokemons.Include(p => p.PokemonAbilitie).Include(p => p.PokemonType).ToListAsync(); }

        public async Task<IReadOnlyList<PokemonType>> GetPokemonTypesAsync()
        { return await this.context.PokemonTypes.ToListAsync(); }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        { return await this.context.ProductBrands.ToListAsync(); }

        public async Task<Product> GetProductIdAsync(int id)
        { return await this.context.Products.Include(p => p.ProductType).Include(p => p.ProductBrand).FirstOrDefaultAsync(p => p.Id == id); }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        { return await this.context.Products.Include(p => p.ProductType).Include(p => p.ProductBrand).ToListAsync(); }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        { return await this.context.ProductTypes.ToListAsync(); }
    }
}