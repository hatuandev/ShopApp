using OrderAPI.Domain.Common;

namespace OrderAPI.Domain.Entities;

public class OrderItem : BaseAuditableEntity
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal? Quantity { get; set; }
    public decimal? Price { get; set; }
}
