using Core.OrderAggregation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(x => x.ShippingAddress, y => {
                y.WithOwner();
            });

            builder.Navigation(a => a.ShippingAddress).IsRequired();

            builder.Property(s => s.OrderStatus).HasConversion(x => x.ToString(), x => (OrderStatus) Enum.Parse(typeof(OrderStatus), x));
            
            builder.HasMany(x => x.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}