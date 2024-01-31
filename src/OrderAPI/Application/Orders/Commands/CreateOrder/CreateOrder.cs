namespace OrderAPI.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand : IRequest<int>
{
    public string? Name { get; init; }
    public DateTimeOffset DateOrder { get; init; }
    public List<CreateOrderItemDto> OrderItems { get; init; } = new List<CreateOrderItemDto>();
}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IGenericRepository<Order> _context;

    public CreateOrderCommandHandler(IGenericRepository<Order> context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = new Order
        {
            Name = request.Name,
            DateOrder = request.DateOrder,
        };

        if (request.OrderItems.Count > 0)
        {
            decimal? total = 0;
            foreach (var item in request.OrderItems)
            {
                var orderItem = new OrderItem();
                orderItem.ProductId = item.ProductId;
                orderItem.Quantity = item.Quantity;
                orderItem.Price = item.Price;

                entity.OrderItems.Add(orderItem);
                total += item.Quantity * item.Price;
            }

            if (total is null)
                entity.Total = 0;
            else
                entity.Total = (decimal)total;
        }    

        _context.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
