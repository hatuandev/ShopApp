using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProductAPI.Infrastructure.Data.Configurations;

public class PickingTypeConfiguration : IEntityTypeConfiguration<PickingType>
{
    public void Configure(EntityTypeBuilder<PickingType> builder)
    {
        builder.ToTable("PickingTypes");
        builder.Property(t => t.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasData(new List<PickingType> {
            new PickingType
            {   
                Id = 1,
                Name = "Receipts",
                Barcode = "WH-RECEIPTS",
                SequenceCode = "IN",
                Created = DateTimeOffset.Now,
                CreatedBy = "Admin",
                LastModified = DateTimeOffset.Now,
                LastModifiedBy = "Admin"
            },
            new PickingType
            {
                Id = 2,
                Name = "Delivery",
                Barcode = "WH-DELIVERY",
                SequenceCode = "OUT",
                Created = DateTimeOffset.Now,
                CreatedBy = "Admin",
                LastModified = DateTimeOffset.Now,
                LastModifiedBy = "Admin"
            }
        });
    }
}
