using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class UserConfiguration:IEntityTypeConfiguration<User>
{
    private readonly UserManager<User> _userManager;
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany<Limit>()
            .WithOne()
            .HasForeignKey(e => e.UserId)
            .IsRequired();
        
        builder.HasMany<Product>()
            .WithOne()
            .HasForeignKey(e => e.UserId)
            .IsRequired();
        
        builder.HasMany<ProductConsumption>()
            .WithOne()
            .HasForeignKey(e => e.UserId)
            .IsRequired();
        
        builder.HasMany<WaterConsumption>()
            .WithOne()
            .HasForeignKey(e => e.UserId)
            .IsRequired();
    }
}