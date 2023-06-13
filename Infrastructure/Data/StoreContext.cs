
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        // Constructor
        public StoreContext(DbContextOptions options) : base(options)
        {
        }

        // Creates a table called Products with rows of a Product and columns according to product attributes/properties
        public DbSet<Product> Products { get; set; }

        public DbSet<Pokemon> Pokemons { get; set; }
    }
}