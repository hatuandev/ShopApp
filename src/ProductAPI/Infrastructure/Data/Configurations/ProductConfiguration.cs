using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProductAPI.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.Property(t => t.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasData(new List<Product> {
            new Product 
            {   Id = 1,
                Name = "Special cotton shirt for men",
                Code = "61ab420c0f34753bcedfa787",
                Cost = 150,
                Description = "Description"
            },
            new Product
            {
                Id = 2,
                Name = "High quality men distress skinny blue jeans",
                Code = "61ab42600f34753bcedfa78b",
                Cost = 250,
                Description = "Description"
            }
        });
    }
}

