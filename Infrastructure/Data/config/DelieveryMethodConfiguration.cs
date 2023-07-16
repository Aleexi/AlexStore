using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.OrderAggregation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.config
{
    public class DelieveryMethodConfiguration : IEntityTypeConfiguration<DelieveryMethod>
    {
        public void Configure(EntityTypeBuilder<DelieveryMethod> builder)
        {
            builder.Property(d => d.PriceOfDelievery).HasColumnType("decimal(18,2)");
        }
    }
}