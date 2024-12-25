using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Orders.Domain.Entities;

namespace Orders.Infrastructure.Persistance;

public class OrderDbContext(DbContextOptions<OrderDbContext> options, IConfiguration configuration) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<LineItem> LineItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(configuration.GetSection("Schemas:OrderSchema").Value);

        modelBuilder.Entity<LineItem>()
            .OwnsOne(p => p.Price, price =>
            {
                price.Property(q => q.Amount).HasColumnName("PriceAmount");
                price.Property(q => q.Unit).HasColumnName("PriceUnit");
            });
    }
}