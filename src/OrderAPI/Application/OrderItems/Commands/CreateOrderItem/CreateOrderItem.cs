using OrderAPI.Domain.Events;

namespace OrderAPI.Application.OrderItems.Commands.CreateOrderItem;

public record CreateOrderItemCommand : IRequest<int>
{
    public int OrderId { get; init; }
    public int ProductId { get; init; }
    public decimal? Quantity { get; init; }
    public decimal? Price { get; init; }
}

public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, int>
{
    private readonly IGenericRepository<OrderItem> _context;

    public CreateOrderItemCommandHandler(IGenericRepository<OrderItem> context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new OrderItem
        {
            OrderId = request.OrderId,
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            Price = request.Price, 
        };

        entity.AddDomainEvent(new OrderItemSumTotalEvent(entity));

        _context.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}