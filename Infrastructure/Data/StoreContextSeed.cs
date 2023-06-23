using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context){

            if (!context.PokemonTypes.Any())
            {
                var pokemonTypesData = File.ReadAllText("../Infrastructure/Data/Seed/pokemontypes.json");
                var pokemonsTypes = JsonSerializer.Deserialize<List<PokemonType>>(pokemonTypesData);
                context.PokemonTypes.AddRange(pokemonsTypes);
            }

            if (!context.PokemonAbilities.Any())
            {
                var pokemonAbilitiesData = File.ReadAllText("../Infrastructure/Data/Seed/pokemonabilities.json");
                var pokemonsAbilities = JsonSerializer.Deserialize<List<PokemonAbilitie>>(pokemonAbilitiesData);
                context.PokemonAbilities.AddRange(pokemonsAbilities);
            }
            
            if (!context.ProductTypes.Any())
            {
                var productTypesData = File.ReadAllText("../Infrastructure/Data/Seed/types.json");
                var productTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypesData);
                context.ProductTypes.AddRange(productTypes);
            }

            if (!context.ProductBrands.Any())
            {
                var productBrandsData = File.ReadAllText("../Infrastructure/Data/Seed/brands.json");
                var productBrands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandsData);
                context.ProductBrands.AddRange(productBrands);
            }
            
            if (!context.Pokemons.Any())
            {
                var pokemonsData = File.ReadAllText("../Infrastructure/Data/Seed/pokemons.json");
                var pokemons = JsonSerializer.Deserialize<List<Pokemon>>(pokemonsData);
                context.Pokemons.AddRange(pokemons);
            }
            
            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText("../Infrastructure/Data/Seed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                context.Products.AddRange(products);
            }
            
            if (context.ChangeTracker.HasChanges()){
                await context.SaveChangesAsync();
            }
        }
    }
}