namespace ProductAPI.Application.Products.Commands.CreateProduct;

public record CreateProductCommand : IRequest<int>
{
    public string? Name { get; init; }
    public string? Code { get; init; }
    public decimal? Cost { get; init; }
    public ProductType? ProductType { get; init; }
    public string? Description { get; init; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IGenericRepository<Product> _context;

    public CreateProductCommandHandler(IGenericRepository<Product> context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = new Product
        {
            Name = request.Name,
            Code = request.Code,
            Cost = request.Cost,
            ProductType = request.ProductType,
            Description = request.Description,
        };

        _context.Add(entity);
        await _context.CommitAsync();
        return entity.Id;
    }
}
