
namespace OrderAPI.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand : IRequest
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public DateTimeOffset DateOrder { get; init; }
    public decimal Total { get; init; }
    public List<OrderItem> OrderItems { get; init; } = new List<OrderItem>();
}

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IGenericRepository<Order> _context;

    public UpdateOrderCommandHandler(IGenericRepository<Order> context)
    {
        _context = context;
    }

    public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.GetByIdAsync(request.Id);
        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;
        entity.DateOrder = request.DateOrder;

        _context.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}