using OrderAPI.Domain.Common;

namespace OrderAPI.Domain.Entities;

public class Order : BaseAuditableEntity
{
    public string? Name { get; set; }
    public DateTimeOffset DateOrder { get; set; }
    public decimal Total { get; set; }
    public IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
