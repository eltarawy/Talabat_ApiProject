using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatG02.Core.Entities.OrderAggregtion;

namespace TalabatG02.Repository.Data.Configurations
{
    internal class OrderItemConfguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(OI => OI.Product, product => product.WithOwner());
            builder.Property(OI => OI.Price)
             .HasColumnType("decimal(18,2)");
        }
    }
}
