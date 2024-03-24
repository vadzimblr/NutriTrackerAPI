using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class ProductConsumptionConfiguration:IEntityTypeConfiguration<ProductConsumption>
{
    public void Configure(EntityTypeBuilder<ProductConsumption> builder)
    {
        builder.HasMany<Product>().WithOne().IsRequired();
    }
}