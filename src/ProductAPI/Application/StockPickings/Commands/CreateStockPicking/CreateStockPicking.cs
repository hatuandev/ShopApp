
using ProductAPI.Domain.Entities;

namespace ProductAPI.Application.StockPickings.Commands.CreateStockPicking;

public record CreateStockPickingCommand : IRequest<int>
{
    public string? Name { get; init; }
    public DateTimeOffset? ScheduledDate { get; init; }
    public int OrderId { get; init; }
    public int PickingTypeId { get; init; }
    public List<StockMove> StockMoves { get; init; } = new List<StockMove>();
}

public class CreateStockPickingCommandHandler : IRequestHandler<CreateStockPickingCommand, int>
{
    private readonly IGenericRepository<StockPicking> _context;

    public CreateStockPickingCommandHandler(IGenericRepository<StockPicking> context)
    {
        _context = context;
    }
    public async Task<int> Handle(CreateStockPickingCommand request, CancellationToken cancellationToken)
    {
        var entity = new StockPicking
        {
            Name = request.Name,
            ScheduledDate = request.ScheduledDate,
            OrderId = request.OrderId,
            PickingTypeId = request.PickingTypeId
        };

        if (request.StockMoves.Count > 0)
        {
            foreach (var item in request.StockMoves)
            {
                var move = new StockMove();
                move.StockPickingId = item.StockPickingId;
                move.ProductId = item.ProductId;
                move.Quantity = item.Quantity;
                move.Unit = item.Unit;
                entity.StockMoves.Add(move);
            }
        }

        _context.Add(entity);
        await _context.CommitAsync();
        return entity.Id;
    }
}
