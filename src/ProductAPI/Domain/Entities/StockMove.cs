namespace ProductAPI.Domain.Entities;

public class StockMove : BaseAuditableEntity
{
    public int StockPickingId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string? Unit { get; set; }
}
