using ProductAPI.Domain.Common;

namespace ProductAPI.Domain.Entities;

public class Product : BaseAuditableEntity
{
    public string? Name { get; set; }

    public string? Code { get; set; }

    public decimal? Cost { get; set; }

    public ProductType? ProductType { get; set; }

    public string? Description { get; set; }
}
