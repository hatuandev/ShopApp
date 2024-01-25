namespace ProductAPI.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(int Id) : IRequest;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IGenericRepository<Product> _context;

    public DeleteProductCommandHandler(IGenericRepository<Product> context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.GetByIdAsync(request.Id);
        Guard.Against.NotFound(request.Id, entity);

        _context.Remove(entity);
        await _context.CommitAsync();
    }
}