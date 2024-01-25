namespace OrderAPI.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand : IRequest<int>
{
    public string? Name { get; init; }
    public DateTimeOffset DateOrder { get; init; }
    public decimal Total { get; init; }
    public List<OrderItem> OrderItems { get; init; } = new List<OrderItem>();
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
            Total = 0,
        };

        _context.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}