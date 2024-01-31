using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductAPI.Domain.Entities;

namespace ProductAPI.Infrastructure.Data.Configurations;

public class StockMoveConfiguration : IEntityTypeConfiguration<StockMove>
{
    public void Configure(EntityTypeBuilder<StockMove> builder)
    {
        builder.ToTable("StockMoves");
        builder.Property(t => t.ProductId).IsRequired();

        builder.HasData(new List<StockMove> {
            new StockMove
            {
                Id = 1,
                ProductId = 1,
                Quantity = 100,
                Unit="Kg",
                StockPickingId = 1,
                Created = DateTimeOffset.Now,
                CreatedBy = "Admin",
                LastModified = DateTimeOffset.Now,
                LastModifiedBy = "Admin"
            },
            new StockMove
            {
                Id = 2,
                ProductId = 2,
                Quantity = 100,
                Unit="Kg",
                StockPickingId = 2,
                Created = DateTimeOffset.Now,
                CreatedBy = "Admin",
                LastModified = DateTimeOffset.Now,
                LastModifiedBy = "Admin"
            },
        });
    }
}
