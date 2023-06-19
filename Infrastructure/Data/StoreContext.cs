
using System.Reflection;
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

        /* Create tables of corresponding DbSet with the <> as entities/rows and columns according to their member variables, table name after <> */
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductType> ProductTypes{ get; set; }

        public DbSet<ProductBrand> ProductBrands{ get; set; }

        public DbSet<Pokemon> Pokemons { get; set; }

        public DbSet<PokemonAbilitie> PokemonAbilities{ get; set; }

        public DbSet<PokemonType> PokemonTypes{ get; set; }

        // If configurations should be applied 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Check which entities has the property Decimal, convert it to double. Only in the case of a SQLite database
            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));

                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }
                }
            }
        }
    }
}