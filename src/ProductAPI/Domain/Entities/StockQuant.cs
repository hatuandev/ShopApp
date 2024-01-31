namespace ProductAPI.Domain.Entities;

public class StockQuant : BaseAuditableEntity
{
    public DateTimeOffset QuantDate { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string? Unit { get; set; }
}
