using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository;

public class RepositoryContext:IdentityDbContext<User>
{
    public RepositoryContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Limit>? Limits { get; set; }
    public DbSet<Product>? Products { get; set; }
    public DbSet<ProductConsumption>? ProductConsumptions { get; set; }
    public DbSet<WaterConsumption>? WaterConsumptions { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
    }
}