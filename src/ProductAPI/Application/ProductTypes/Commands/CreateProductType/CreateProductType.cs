
namespace ProductAPI.Application.ProductTypes.Commands.CreateProductType;

public record CreateProductTypeCommand : IRequest<int>
{
    public string? Type { get; init; }
}

public class CreateProductTypeCommandHandler : IRequestHandler<CreateProductTypeCommand, int>
{
    private readonly IGenericRepository<ProductType> _context;

    public CreateProductTypeCommandHandler(IGenericRepository<ProductType> context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProductTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = new ProductType {
            Type = request.Type
        };

        await _context.AddAsync(entity);
        await _context.CommitAsync();
        return entity.Id;
    }
}

