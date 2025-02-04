using MassTransit;
using Microsoft.EntityFrameworkCore;
using Outbox.TopupAPI.Entities;

namespace Outbox.TopupAPI.Contexts;

public class TopupAPIContext : DbContext
{
    public DbSet<TopupTransaction> TopupTransactions { get; set; }

    public TopupAPIContext(DbContextOptions<TopupAPIContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}
