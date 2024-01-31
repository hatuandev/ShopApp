using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProductAPI.Infrastructure.Data.Configurations;

public class StockPickingConfiguration : IEntityTypeConfiguration<StockPicking>
{
    public void Configure(EntityTypeBuilder<StockPicking> builder)
    {
        builder.ToTable("StockPickings");
        builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

        builder.HasData(new List<StockPicking> {
            new StockPicking
            {
                Id = 1,
                Name = "WH/IN/00001",
                ScheduledDate = DateTime.Now,
                OrderId = 1,
                PickingTypeId = 1,
                Created = DateTimeOffset.Now,
                CreatedBy = "Admin",
                LastModified = DateTimeOffset.Now,
                LastModifiedBy = "Admin"
            },
            new StockPicking
            {
                Id = 2,
                Name = "WH/IN/00002",
                ScheduledDate = DateTime.Now,
                OrderId = 2,
                PickingTypeId = 1,
                Created = DateTimeOffset.Now,
                CreatedBy = "Admin",
                LastModified = DateTimeOffset.Now,
                LastModifiedBy = "Admin"
            }
        });
    }
}
