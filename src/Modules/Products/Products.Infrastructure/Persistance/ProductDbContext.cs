using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Products.Domain.Entities;

namespace Products.Infrastructure.Persistance;

public class ProductDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ProductDbContext(DbContextOptions<ProductDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(_configuration.GetSection("Schemas:ProductSchema").Value!);

        modelBuilder.Entity<Product>()
            .OwnsOne(p => p.Price, price =>
            {
                price.Property(q => q.Amount).HasColumnName("PriceAmount");
                price.Property(q => q.Unit).HasColumnName("PriceUnit");
            });
    }
}