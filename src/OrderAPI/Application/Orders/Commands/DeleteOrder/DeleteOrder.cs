namespace OrderAPI.Application.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(int Id) : IRequest;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IGenericRepository<Order> _context;

    public DeleteOrderCommandHandler(IGenericRepository<Order> context)
    {
        _context = context;
    }

    public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.GetByIdAsync(request.Id);
        Guard.Against.NotFound(request.Id, entity);

        _context.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
