namespace ProductAPI.Domain.Entities;

public class StockPicking : BaseAuditableEntity
{
    public string? Name { get; set; }
    public DateTimeOffset? ScheduledDate { get; set; }
    public int OrderId { get; set; }
    public int PickingTypeId { get; set; }
    public IList<StockMove> StockMoves { get; set; } = new List<StockMove>();
}

