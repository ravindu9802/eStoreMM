using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Users.Domain.Entities;

namespace Users.Infrastructure.Persistance;

public class UserDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public UserDbContext(DbContextOptions<UserDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(_configuration.GetSection("Schemas:UserSchema").Value);

        modelBuilder.Entity<User>(builder => { builder.HasIndex(u => u.Email); });

        base.OnModelCreating(modelBuilder);
    }
}