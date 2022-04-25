using Micro.Samples.OrdersService.Entities;
using Micro.Transactions.Inbox;
using Micro.Transactions.Outbox;
using Microsoft.EntityFrameworkCore;

namespace Micro.Samples.OrdersService.DAL;

public class OrdersDbContext : DbContext
{
    public DbSet<InboxMessage> Inbox { get; set; } = null!;
    public DbSet<OutboxMessage> Outbox { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;

    public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}