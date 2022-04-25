using Micro.Samples.OrdersService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Micro.Samples.OrdersService.DAL.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasOne<Customer>().WithMany().HasForeignKey(x => x.CustomerId);
    }
}