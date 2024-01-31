namespace ProductAPI.Application.StockPickings.Queries.GetStockPickingById;

public record GetStockPickingByIdQuery(int Id) : IRequest<GetStockPickingByIdDto>;

public class GetStockPickingByIdQueryHandler : IRequestHandler<GetStockPickingByIdQuery, GetStockPickingByIdDto>
{
    private readonly IGenericRepository<StockPicking> _context;
    private readonly IMapper _mapper;

    public GetStockPickingByIdQueryHandler(IGenericRepository<StockPicking> context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetStockPickingByIdDto> Handle(GetStockPickingByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.GetByIdAsync(request.Id);
        Guard.Against.NotFound(request.Id, entity);

        var stockPicking = _mapper.Map<GetStockPickingByIdDto>(entity);
        return stockPicking;
    }
}
