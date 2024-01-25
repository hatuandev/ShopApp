namespace ProductAPI.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand : IRequest
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? Code { get; init; }
    public decimal Cost { get; init; }
    public ProductType? ProductType { get; init; }
    public string? Description { get; init; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IGenericRepository<Product> _context;

    public UpdateProductCommandHandler(IGenericRepository<Product> context)
    {
        _context = context;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.GetByIdAsync(request.Id);
        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;
        entity.Code = request.Code;
        entity.Cost = request.Cost;
        entity.ProductType = request.ProductType;
        entity.Description = request.Description;

        _context.Update(entity);
        await _context.CommitAsync();
    }
}