using Micro.Samples.CustomersService.Entities;
using Micro.Transactions.Inbox;
using Micro.Transactions.Outbox;
using Microsoft.EntityFrameworkCore;

namespace Micro.Samples.CustomersService.DAL;

public class CustomersDbContext : DbContext
{
    public DbSet<InboxMessage> Inbox { get; set; } = null!;
    public DbSet<OutboxMessage> Outbox { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;

    public CustomersDbContext(DbContextOptions<CustomersDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}