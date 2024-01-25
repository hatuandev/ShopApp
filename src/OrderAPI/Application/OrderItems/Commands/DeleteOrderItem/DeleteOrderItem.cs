namespace OrderAPI.Application.OrderItems.Commands.DeleteOrderItem;

public record DeleteOrderItemCommand(int Id, int OrderId) : IRequest;

public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand>
{
    private readonly IGenericRepository<OrderItem> _context;

    public DeleteOrderItemCommandHandler(IGenericRepository<OrderItem> context)
    {
        _context = context;
    }

    public async Task Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
    {
        var entity = _context.GetAll(x => x.Id == request.Id && x.OrderId == request.OrderId).FirstOrDefault();
        Guard.Against.NotFound(request.Id, entity);

        _context.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}