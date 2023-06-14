using Core.Entities;

namespace Core.Interfaces
{
    public interface InterfaceRepository
    {
        Task<Product> GetProductIdAsync(int id);
        
        // Return a list which cannot be modified only read from
        Task<IReadOnlyList<Product>> GetProductsAsync();

        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
        
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();

        
        Task<Pokemon> GetPokemonNameAsync(string name);

        // Return a list which cannot be modified only read from
        Task<IReadOnlyList<Pokemon>> GetPokemonsAsync();

        Task<IReadOnlyList<PokemonType>> GetPokemonTypesAsync();
        
        Task<IReadOnlyList<PokemonAbilitie>> GetPokemonAbilitiesAsync();

    }
}