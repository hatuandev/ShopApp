namespace ProductAPI.Application.ProductTypes.Commands.UpdateProductType;

public record UpdateProductTypeCommand : IRequest
{
    public int Id { get; init; }
    public string? Type { get; init; }
}

public class UpdateProductTypeCommandHandler : IRequestHandler<UpdateProductTypeCommand>
{
    private readonly IGenericRepository<ProductType> _context;

    public UpdateProductTypeCommandHandler(IGenericRepository<ProductType> context)
    {
        _context = context;
    }

    public async Task Handle(UpdateProductTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.GetByIdAsync(request.Id);
        Guard.Against.NotFound(request.Id, entity);

        entity.Type = request.Type;
        _context.Update(entity);
        await _context.CommitAsync();
    }
}
