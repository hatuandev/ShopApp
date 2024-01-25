namespace ProductAPI.Application.ProductTypes.Commands.DeleteProductType;

public record DeleteProductTypeCommand(int Id) : IRequest;

public class DeleteProductTypeCommandHandler : IRequestHandler<DeleteProductTypeCommand>
{
    private readonly IGenericRepository<ProductType> _context;

    public DeleteProductTypeCommandHandler(IGenericRepository<ProductType> context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProductTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.GetByIdAsync(request.Id);
        Guard.Against.NotFound(request.Id, entity);

        _context.Remove(entity);
        await _context.CommitAsync();
    }
}
