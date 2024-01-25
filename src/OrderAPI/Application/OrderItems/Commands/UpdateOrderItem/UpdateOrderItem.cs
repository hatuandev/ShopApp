namespace OrderAPI.Application.OrderItems.Commands.UpdateOrderItem;

public record UpdateOrderItemCommand : IRequest
{
    public int Id { get; init; }
    public int OrderId { get; init; }
    public int ProductId { get; init; }
    public decimal? Quantity { get; init; }
    public decimal? Price { get; init; }
}

public class UpdateOrderItemCommandHandler : IRequestHandler<UpdateOrderItemCommand>
{
    private readonly IGenericRepository<OrderItem> _context;

    public UpdateOrderItemCommandHandler(IGenericRepository<OrderItem> context)
    {
        _context = context;
    }

    public async Task Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
    {
        var entity = _context.GetAll(x => x.Id == request.Id && x.OrderId == request.OrderId).FirstOrDefault();
        Guard.Against.NotFound(request.Id, entity);

        entity.ProductId = request.ProductId;
        entity.Quantity = request.Quantity;
        entity.Price = request.Price;

        _context.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}