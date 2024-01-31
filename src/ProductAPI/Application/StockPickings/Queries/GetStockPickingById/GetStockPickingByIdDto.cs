namespace ProductAPI.Application.StockPickings.Queries.GetStockPickingById;

public class GetStockPickingByIdDto
{
    public string? Name { get; init; }
    public DateTimeOffset? ScheduledDate { get; init; }
    public int OrderId { get; init; }
    public int PickingTypeId { get; init; }
    public IList<StockMove> StockMoves { get; init; } = new List<StockMove>();

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<StockPicking, GetStockPickingByIdDto>();
        }
    }
}
